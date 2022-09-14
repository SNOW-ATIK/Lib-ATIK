using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.Threading;

using ATIK.Device.IO;

using CrevisFnIoLib;

namespace ATIK.Device.Fieldbus.Crevis
{
    public static partial class Fieldbus_CrevisModbusTCP
    {
        public class SingleModbusTCP
        {
            public bool IsInitialized { get; private set; }
            private int IOUpdateLoopPeriod_ms = 100;
            private IntPtr m_hSystem;
            private IntPtr m_hDevice;
            private ConcurrentDictionary<int, List<Element_IO>> ModbusSingleModule; // Key:Slot, Value:Elements
            private ConcurrentDictionary<int, SlotInfo> SlotInfos;

            private int SizeOf_Input = 0;
            private int SizeOf_Output = 0;

            private Thread IOUpdateThread;
            private ManualResetEventSlim Flag_Reading = new ManualResetEventSlim(false);

            private ConcurrentDictionary<int, (int DataStartOffset, byte[] Data)> Data_ToBeWritten;
            private ConcurrentDictionary<int, (int DataStartOffset, byte[] Data)> Data_ReadWritten;
            private ConcurrentDictionary<int, (int DataStartOffset, byte[] Data)> Data_Read;

            public SingleModbusTCP(IntPtr psystem, IntPtr pdevice, ConcurrentDictionary<int, List<Element_IO>> modbusIOModule)
            {
                m_hSystem = psystem;
                m_hDevice = pdevice;
                ModbusSingleModule = modbusIOModule;

                SlotInfos = new ConcurrentDictionary<int, SlotInfo>();
                ModbusSingleModule.Values.ToList().ForEach(list =>
                {
                    list.ForEach(elem =>
                    {
                        SlotInfo slotInfo = (SlotInfo)elem.Setting;
                        if (SlotInfos.Keys.Contains(slotInfo.SlotIndex) == false)
                        {
                            SlotInfos.TryAdd(slotInfo.SlotIndex, slotInfo);
                        }
                    });
                });

                SlotInfos.Values.ToList().ForEach(slot =>
                {
                    switch (slot.SlotType)
                    {
                        case SlotType.AIN:
                        case SlotType.DIN:
                            SizeOf_Input += slot.SizeOfBits;
                            break;

                        case SlotType.AOUT:
                        case SlotType.DOUT:
                            SizeOf_Output += slot.SizeOfBits;
                            break;
                    }
                });

                SizeOf_Input /= 8;
                SizeOf_Output /= 8;

                Data_ToBeWritten = new ConcurrentDictionary<int, (int DataStartOffset, byte[] Data)>();
                Data_ReadWritten = new ConcurrentDictionary<int, (int DataStartOffset, byte[] Data)>();
                Data_Read = new ConcurrentDictionary<int, (int DataStartOffset, byte[] Data)>();
                int startOffset_Write = 0;
                int startOffset_Read = 0;
                SlotInfos.Keys.ToList().ForEach(slotIdx =>
                {
                    switch (SlotInfos[slotIdx].SlotType)
                    {
                        case SlotType.AIN:
                        case SlotType.DIN:
                            Data_Read.TryAdd(slotIdx, (startOffset_Read, new byte[SlotInfos[slotIdx].SizeOfBits / 8]));

                            startOffset_Read += SlotInfos[slotIdx].SizeOfBits / 8;
                            break;

                        case SlotType.AOUT:
                        case SlotType.DOUT:
                            Data_ToBeWritten.TryAdd(slotIdx, (startOffset_Write, new byte[SlotInfos[slotIdx].SizeOfBits / 8]));
                            Data_ReadWritten.TryAdd(slotIdx, (startOffset_Write, new byte[SlotInfos[slotIdx].SizeOfBits / 8]));

                            startOffset_Write += SlotInfos[slotIdx].SizeOfBits / 8;
                            break;
                    }
                });
                IsInitialized = true;
            }

            public bool Start_IOUpdate(int updateInterval_ms = 100)
            {
                m_Err = m_cFnIo.FNIO_DevSetParam(m_hDevice, CrevisFnIO.DEV_UPDATE_FREQUENCY, updateInterval_ms);
                if (m_Err != CrevisFnIO.FNIO_ERROR_SUCCESS)
                {
                    return false;
                }

                m_Err = m_cFnIo.FNIO_DevIoUpdateStart(m_hDevice, CrevisFnIO.IO_UPDATE_PERIODIC);
                if (m_Err != CrevisFnIO.FNIO_ERROR_SUCCESS)
                    return false;

                Flag_Reading.Set();
                IOUpdateThread = new Thread(IOUpdateProcess) { IsBackground = true };
                IOUpdateThread.Start();

                return true;
            }

            public bool Stop_IOUpdate()
            {
                Flag_Reading.Reset();
                if (IOUpdateThread.Join(5000) == false)
                {
                    // #. Invalid
                    IOUpdateThread.Abort();
                }

                m_Err = m_cFnIo.FNIO_DevIoUpdateStop(m_hDevice);
                if (m_Err != CrevisFnIO.FNIO_ERROR_SUCCESS)
                    return false;
                return true;
            }

            public ConcurrentDictionary<int, (int DataStartOffset, byte[] Data)> GetInput_Full()
            {
                return Data_Read;
            }

            public (bool GetSucess, AnalogCurrentRange[] CurrentRanges) GetParam_AnalogIn(int slotIdx, int noOfChannelsInSlot)
            {
                IntPtr tgtSlot = IntPtr.Zero;
                m_Err = CrevisFnIO.IoGetIoModule(m_hDevice, slotIdx, ref tgtSlot);
                if (m_Err != CrevisFnIO.FNIO_ERROR_SUCCESS)
                {
                    // #. Invalid
                    return (false, null);
                }

                int readData = 0;
                int dataSize = 6;
                m_Err = m_cFnIo.FNIO_IoGetParam(tgtSlot, CrevisFnIO.IO_CONFIGURATION_PARAMETER, ref readData, dataSize);
                if (m_Err != CrevisFnIO.FNIO_ERROR_SUCCESS)
                {
                    // #. Invalid
                    return (false, null);
                }

                byte[] datas = BitConverter.GetBytes(readData);
                AnalogCurrentRange[] ranges = new AnalogCurrentRange[noOfChannelsInSlot];
                for (int i = 0; i < noOfChannelsInSlot; i++)
                {
                    ranges[i] = (AnalogCurrentRange)datas[i];
                }

                return (true, ranges);
            }

            public bool SetParam_AnalogIn(int slotIdx, AnalogCurrentRange[] currentRanges)
            {
                IntPtr tgtSlot = IntPtr.Zero;
                m_Err = CrevisFnIO.IoGetIoModule(m_hDevice, slotIdx, ref tgtSlot);
                if (m_Err != CrevisFnIO.FNIO_ERROR_SUCCESS)
                {
                    // #. Invalid
                    return false;
                }

                byte[] datas = new byte[currentRanges.Length + 2];
                for (int i = 0; i < currentRanges.Length; i++)
                {
                    datas[i] = (byte)currentRanges[i];
                }
                int setData = BitConverter.ToInt32(datas, 0);
                int dataSize = 6;

                m_Err = m_cFnIo.FNIO_IoSetParam(tgtSlot, CrevisFnIO.IO_CONFIGURATION_PARAMETER, setData, dataSize);
                if (m_Err != CrevisFnIO.FNIO_ERROR_SUCCESS)
                {
                    // #. Invalid
                    return false;
                }
                return true;
            }

            public byte[] GetInput_Slot(int slotIdx)
            {
                return Data_Read[slotIdx].Data;
            }

            public bool GetInput_Bit(int slotIdx, int bitOrder)
            {
                return new BitArray(Data_Read[slotIdx].Data).Get(bitOrder);
            }

            public int GetInput_Analog(int slotIdx, int channel)
            {
                return (Data_Read[slotIdx].Data[channel * 2 + 0] << 0) + (Data_Read[slotIdx].Data[channel * 2 + 1] << 8);
            }

            public ConcurrentDictionary<int, (int DataStartOffset, byte[] Data)> GetOutput_Full()
            {
                return Data_ReadWritten;
            }

            public byte[] GetOutput_Slot(int slotIdx)
            {
                return Data_ReadWritten[slotIdx].Data;
            }

            public bool GetOutput_Bit(int slotIdx, int bitOrder)
            {
                return new BitArray(Data_ReadWritten[slotIdx].Data).Get(bitOrder);
            }

            public int GetOutput_Analog(int slotIdx, int channel)
            {
                return (Data_ReadWritten[slotIdx].Data[channel * 2 + 0] << 0) + (Data_ReadWritten[slotIdx].Data[channel * 2 + 1] << 8);
            }

            // TBD
            public void SetOutput_Full(ConcurrentDictionary<int, (int DataStartOffset, byte[] Data)> dataFull)
            { 
            }

            public void SetOutput_Slot(int slotIdx, byte[] data)
            {
                Array.Copy(data, 0, Data_ToBeWritten[slotIdx].Data, Data_ToBeWritten[slotIdx].DataStartOffset, data.Length);
            }

            public void SetOutput_Bit(int slotIdx, int bitOrder, bool value)
            {
                BitArray buffTemp = new BitArray(Data_ToBeWritten[slotIdx].Data);
                buffTemp.Set(bitOrder, value);
                buffTemp.CopyTo(Data_ToBeWritten[slotIdx].Data, 0);
            }

            public void SetOutput_Analog(int slotIdx, int channel, int value)
            {
                byte data_0 = (byte)((value & 0x00ff) >> 0);
                byte data_1 = (byte)((value & 0xff00) >> 8);
                Data_ToBeWritten[slotIdx].Data[channel * 2 + 0] = data_0;
                Data_ToBeWritten[slotIdx].Data[channel * 2 + 1] = data_1;
            }

            public bool Close()
            {
                return true;
            }

            public bool Release()
            {
                return true;
            }

            private void IOUpdateProcess()
            {
                System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
                while (Flag_Reading.IsSet == true)
                {
                    st.Reset();
                    st.Start();

                    /* Read - Input ***************************************************************************/
                    byte[] buffRead_Input = new byte[SizeOf_Input];
                    m_Err = m_cFnIo.FNIO_DevReadInputImage(m_hDevice, 0, ref buffRead_Input[0], buffRead_Input.Length);
                    if (m_Err != CrevisFnIO.FNIO_ERROR_SUCCESS)
                    {
                        // #. Invalid
                    }
                    Data_Read.Keys.ToList().ForEach(slotIdx =>
                    {
                        Array.Copy(buffRead_Input, Data_Read[slotIdx].DataStartOffset, Data_Read[slotIdx].Data, 0, Data_Read[slotIdx].Data.Length);
                    });
                    Array.Clear(buffRead_Input, 0, buffRead_Input.Length);
                    //Console.WriteLine("Input: " + ConvertByteToHexString(buffInput));


                    /* Read - Output ***************************************************************************/
                    byte[] buffRead_Output = new byte[SizeOf_Output];
                    m_Err = m_cFnIo.FNIO_DevReadOutputImage(m_hDevice, 0, ref buffRead_Output[0], buffRead_Output.Length);
                    if (m_Err != CrevisFnIO.FNIO_ERROR_SUCCESS)
                    {
                        // #. Invalid
                    }
                    Data_ReadWritten.Keys.ToList().ForEach(slotIdx =>
                    {
                        Array.Copy(buffRead_Output, Data_ReadWritten[slotIdx].DataStartOffset, Data_ReadWritten[slotIdx].Data, 0, Data_ReadWritten[slotIdx].Data.Length);
                    });
                    Array.Clear(buffRead_Output, 0, buffRead_Output.Length);
                    //Console.WriteLine("Output: " + ConvertByteToHexString(buffOutput));


                    /* Write - Output ***************************************************************************/
                    if (Data_ToBeWritten.SequenceEqual(Data_ReadWritten) == false)
                    {
                        byte[] buffWrite_Output = new byte[SizeOf_Output];
                        Data_ToBeWritten.Keys.ToList().ForEach(slotIdx =>
                        {
                            //Array.Copy(Data_ToBeWritten[slotIdx].Data, Data_ToBeWritten[slotIdx].DataStartOffset, buffWrite_Output, 0, Data_ToBeWritten[slotIdx].Data.Length);
                            Array.Copy(Data_ToBeWritten[slotIdx].Data, 0, buffWrite_Output, Data_ToBeWritten[slotIdx].DataStartOffset, Data_ToBeWritten[slotIdx].Data.Length);
                        });
                        m_Err = m_cFnIo.FNIO_DevWriteOutputImage(m_hDevice, 0, ref buffWrite_Output[0], buffWrite_Output.Length);
                        if (m_Err != CrevisFnIO.FNIO_ERROR_SUCCESS)
                        {
                            // #. Invalid
                        }
                        Array.Clear(buffWrite_Output, 0, buffWrite_Output.Length);
                    }

                    st.Stop();

                    int extraTime = (int)(IOUpdateLoopPeriod_ms - st.ElapsedMilliseconds);
                    if (extraTime > 0)
                    {
                        Thread.Sleep(extraTime);
                    }
                    else
                    {
                        // #. Invalid
                    }
                }
            }
        }
    }
}
