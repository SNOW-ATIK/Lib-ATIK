using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using ATIK.Device;
using ATIK.Device.IO;

using CrevisFnIoLib;

namespace ATIK.Device.Fieldbus.Crevis
{
    public static partial class Fieldbus_CrevisModbusTCP
    {

        public class ModbusTCPInfo
        {
            public readonly int ModbusIndex;
            public readonly string IPAddress;
            public ModbusTCPInfo(int modbusIdx, string ipAddress)
            {
                ModbusIndex = modbusIdx;
                IPAddress = ipAddress;
            }
        }

        public class SlotInfo
        {
            public readonly ModbusTCPInfo ModbusTCPInfo;
            public readonly int SlotIndex = -1;
            public readonly string SlotName = string.Empty;
            public readonly SlotType SlotType = SlotType.Unknown;
            public readonly int SizeOfBits = -1;
            public readonly int ChannelCounts = -1;
            public SlotInfo(ModbusTCPInfo modbusTCPInfo, int slotIdx, string slotName, SlotType slotType, int sizeOfBits, int channeCounts)
            {
                ModbusTCPInfo = modbusTCPInfo;
                SlotName = slotName;
                SlotIndex = slotIdx;
                SlotType = slotType;
                SizeOfBits = sizeOfBits;
                ChannelCounts = channeCounts;
            }
        }

        public static bool IsInitialized
        {
            get;
            private set;
        }

        private static ConcurrentDictionary<int, ConcurrentDictionary<int, List<Element_IO>>> Dic_ModbusIOModules;


        private static CrevisFnIO m_cFnIo = new CrevisFnIO();
        private static Int32 m_Err = 0;
        private static IntPtr m_hSystem;
        private static Dictionary<int, string> Dic_IPAddressInfo;
        private static ConcurrentDictionary<string, SingleModbusTCP> Cdic_ModbusModules;

        public static bool Init_CrevisModbusTCP(List<Element_IO> elemInfos, int ioUpdateInterval_ms = 100)
        {
            if (elemInfos.Count > 0)
            {
                Dic_ModbusIOModules = new ConcurrentDictionary<int, ConcurrentDictionary<int, List<Element_IO>>>();                
            }
            else
            {
                return false;
            }

            Dic_IPAddressInfo = new Dictionary<int, string>();
            for (int i = 0; i < elemInfos.Count; i++)
            {
                SlotInfo slotInfo = (SlotInfo)elemInfos[i].Setting;
                int modbusIdx = slotInfo.ModbusTCPInfo.ModbusIndex;
                int slotIdx = slotInfo.SlotIndex;

                if (Dic_ModbusIOModules.ContainsKey(modbusIdx) == false)
                {
                    Dic_ModbusIOModules.TryAdd(modbusIdx, new ConcurrentDictionary<int, List<Element_IO>>());
                }
                if (Dic_ModbusIOModules[modbusIdx].ContainsKey(slotIdx) == false)
                {
                    Dic_ModbusIOModules[modbusIdx].TryAdd(slotIdx, new List<Element_IO>());
                }
                Dic_ModbusIOModules[modbusIdx][slotIdx].Add(elemInfos[i]);

                if (Dic_IPAddressInfo.ContainsKey(modbusIdx) == false)
                {
                    Dic_IPAddressInfo.Add(modbusIdx, slotInfo.ModbusTCPInfo.IPAddress);
                }
            }

            m_Err = m_cFnIo.FNIO_LibInitSystem(ref m_hSystem);
            if (m_Err != CrevisFnIO.FNIO_ERROR_SUCCESS)
            {
                return false;
            }

            Cdic_ModbusModules = new ConcurrentDictionary<string, SingleModbusTCP>();
            int nInitialized = 0;
            Dic_ModbusIOModules.Keys.ToList().ForEach(modbusIdx =>
            {
                IntPtr pDevice = IntPtr.Zero;
                if (Init_SingleModbusTCP(Dic_IPAddressInfo[modbusIdx], ref pDevice) == false)
                {
                    return;
                }

                SingleModbusTCP singleModbus = new SingleModbusTCP(m_hSystem, pDevice, Dic_ModbusIOModules[modbusIdx]);
                if (singleModbus.IsInitialized == true)
                {
                    if (singleModbus.Start_IOUpdate(ioUpdateInterval_ms) == true)
                    {
                        Cdic_ModbusModules.TryAdd(Dic_IPAddressInfo[modbusIdx], singleModbus);
                        ++nInitialized;
                    }
                }
            });

            if (nInitialized == Dic_ModbusIOModules.Count)
            {
                IsInitialized = true;
            }
            else
            {
                IsInitialized = false;
            }
            return IsInitialized;
        }

        public static (bool getSuccess, bool state) Get_DigitalValue(Element_IO elem)
        {
            if (IsInitialized == false)
            {
                return (false, false);
            }
            bool rtnGetSuccess = false;
            bool rtnState = false;

            SlotInfo slotInfo = (SlotInfo)elem.Setting;
            if ((slotInfo.SlotType == SlotType.DIN && elem.Type == IOType.DIN)
                || (slotInfo.SlotType == SlotType.DOUT && elem.Type == IOType.DOUT))
            {
                switch (slotInfo.SlotType)
                {
                    case SlotType.DIN:
                        rtnState = Cdic_ModbusModules[slotInfo.ModbusTCPInfo.IPAddress].GetInput_Bit(slotInfo.SlotIndex, elem.ChannelNo);
                        break;

                    case SlotType.DOUT:
                        rtnState = Cdic_ModbusModules[slotInfo.ModbusTCPInfo.IPAddress].GetOutput_Bit(slotInfo.SlotIndex, elem.ChannelNo);
                        break;
                }
                rtnGetSuccess = true;
            }
            else
            {
                rtnGetSuccess = false;
            }

            return (rtnGetSuccess, rtnState);
        }

        public static bool Set_DigitalValue(Element_IO elem, bool value, bool sync = false, int syncTimeOut_ms = 500)
        {
            if (IsInitialized == false)
            {
                return false;
            }
            bool rtnSetSuccess = false;

            SlotInfo slotInfo = (SlotInfo)elem.Setting;
            if (slotInfo.SlotType == SlotType.DOUT && elem.Type == IOType.DOUT)
            {
                Cdic_ModbusModules[slotInfo.ModbusTCPInfo.IPAddress].SetOutput_Bit(slotInfo.SlotIndex, elem.ChannelNo, value);
                if (sync == true)
                {
                    System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
                    st.Start();
                    while (rtnSetSuccess == false && st.ElapsedMilliseconds < syncTimeOut_ms)
                    {
                        rtnSetSuccess = Cdic_ModbusModules[slotInfo.ModbusTCPInfo.IPAddress].GetOutput_Bit(slotInfo.SlotIndex, elem.ChannelNo) == value;
                        Thread.Sleep(10);
                    }
                    st.Stop();
                }
                rtnSetSuccess = true;
            }

            return rtnSetSuccess;
        }

        public static (bool GetSuccess, AnalogCurrentRange CurrentRange) GetParam_AnalogIn(Element_IO elem)
        {
            if (IsInitialized == false)
            {
                return (false, AnalogCurrentRange.Unknown);
            }
            bool rtnGetSuccess = false;
            AnalogCurrentRange rtnValue = AnalogCurrentRange.Unknown;

            SlotInfo slotInfo = (SlotInfo)elem.Setting;
            if ((slotInfo.SlotType == SlotType.AIN && elem.Type == IOType.AIN)
                || (slotInfo.SlotType == SlotType.AOUT && elem.Type == IOType.AOUT))
            {
                switch (slotInfo.SlotType)
                {
                    case SlotType.AIN:
                        var read = Cdic_ModbusModules[slotInfo.ModbusTCPInfo.IPAddress].GetParam_AnalogIn(slotInfo.SlotIndex, slotInfo.ChannelCounts);
                        if (read.GetSucess == true)
                        {
                            rtnValue = read.CurrentRanges[elem.ChannelNo];
                            rtnGetSuccess = true;
                        }
                        break;
                }
            }
            return (rtnGetSuccess, rtnValue);
        }

        public static bool SetParam_AnalogIn(Element_IO elem, AnalogCurrentRange currentRange)
        {
            if (IsInitialized == false)
            {
                return false;
            }

            bool rtnSetSuccess = false;
            SlotInfo slotInfo = (SlotInfo)elem.Setting;
            if (slotInfo.SlotType == SlotType.AIN && elem.Type == IOType.AIN)
            {
                switch (slotInfo.SlotType)
                {
                    case SlotType.AIN:
                        var read = Cdic_ModbusModules[slotInfo.ModbusTCPInfo.IPAddress].GetParam_AnalogIn(slotInfo.SlotIndex, slotInfo.ChannelCounts);
                        if (read.GetSucess == true)
                        {
                            AnalogCurrentRange[] setData = new AnalogCurrentRange[read.CurrentRanges.Length];
                            for (int i = 0; i < read.CurrentRanges.Length; i++)
                            {
                                if (i == elem.ChannelNo)
                                {
                                    setData[i] = currentRange;
                                }
                                else
                                {
                                    setData[i] = read.CurrentRanges[i];
                                }
                            }
                            rtnSetSuccess = Cdic_ModbusModules[slotInfo.ModbusTCPInfo.IPAddress].SetParam_AnalogIn(slotInfo.SlotIndex, setData);
                        }
                        break;
                }
            }

            return rtnSetSuccess;
        }

        public static (bool getSuccess, int value) Get_AnalogValue(Element_IO elem)
        {
            if (IsInitialized == false)
            {
                return (false, -1);
            }

            bool rtnGetSuccess = false;
            int rtnValue = 0;
            SlotInfo slotInfo = (SlotInfo)elem.Setting;
            if ((slotInfo.SlotType == SlotType.AIN && elem.Type == IOType.AIN)
                || (slotInfo.SlotType == SlotType.AOUT && elem.Type == IOType.AOUT))
            {
                switch (slotInfo.SlotType)
                {
                    case SlotType.AIN:
                        rtnValue = Cdic_ModbusModules[slotInfo.ModbusTCPInfo.IPAddress].GetInput_Analog(slotInfo.SlotIndex, elem.ChannelNo);
                        break;

                    case SlotType.AOUT:
                        rtnValue = Cdic_ModbusModules[slotInfo.ModbusTCPInfo.IPAddress].GetOutput_Analog(slotInfo.SlotIndex, elem.ChannelNo);
                        break;
                }
                rtnGetSuccess = true;
            }

            return (rtnGetSuccess, rtnValue);
        }

        public static bool Set_AnalogValue(Element_IO elem, int value)
        {
            if (IsInitialized == false)
            {
                return false;
            }
            bool rtnSetSuccess = false;

            SlotInfo slotInfo = (SlotInfo)elem.Setting;
            if (slotInfo.SlotType == SlotType.AOUT && elem.Type == IOType.AOUT)
            {
                Cdic_ModbusModules[slotInfo.ModbusTCPInfo.IPAddress].SetOutput_Analog(slotInfo.SlotIndex, elem.ChannelNo, value);
                rtnSetSuccess = true;
            }

            return rtnSetSuccess;
        }

        private static bool Init_SingleModbusTCP(string ipAddress, ref IntPtr pdevice)
        {
            string[] words = ipAddress.Split('.');
            if (words.Length != 4)
            {
                return false;
            }

            CrevisFnIO.DEVICEINFOMODBUSTCP2 DeviceInfo = new CrevisFnIO.DEVICEINFOMODBUSTCP2();
            DeviceInfo.IpAddress = new byte[4];
            for (int i = 0; i < words.Length; i++)
            {
                DeviceInfo.IpAddress[i] = (byte)(Int32.Parse(words[i]));
            }

            m_Err = m_cFnIo.FNIO_DevOpenDevice(m_hSystem, ref DeviceInfo, CrevisFnIO.MODBUS_TCP, ref pdevice);
            if (m_Err != CrevisFnIO.FNIO_ERROR_SUCCESS)
            {
                m_cFnIo.FNIO_LibFreeSystem(m_hSystem);
                return false;
            }

            return true;
        }
    }
}
