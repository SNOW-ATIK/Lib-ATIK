using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Timers;

using ATIK.Device.Fieldbus.Crevis;

namespace ATIK.Device.IO
{
    public class Element_IO : INotifyPropertyChanged
    {
        private static XmlCfg_IO XmlConfig;
        private static ConcurrentDictionary<IODriver, List<Element_IO>> Dic_ElementAll;
        private static ConcurrentDictionary<string, Element_IO> Dic_ElementsByLogicalName = new ConcurrentDictionary<string, Element_IO>();
        private static ConcurrentDictionary<IODriver, bool> Dic_IsInitialized;

        private static Timer tmr_ReadContinuously = new Timer();

        public static bool Load_Config(string xmlCfgFilePath, string xmlCfgFileName, string xmlRootName = "")
        {
            if (File.Exists(Path.Combine(xmlCfgFilePath, xmlCfgFileName)) == false)
            {
                return false;
            }

            if (string.IsNullOrEmpty(xmlRootName) == true)
            {
                xmlRootName = "XmlCfg_IO";
            } 
            XmlCfgPrm xmlCfgPrm = new XmlCfgPrm(xmlCfgFilePath, xmlCfgFileName, xmlRootName);
            if (xmlCfgPrm == null)
            {
                return false;
            }

            int NoOfDrivers = int.Parse(xmlCfgPrm.Get_Item("NoOfDrivers"));
            if (NoOfDrivers > 0)
            {
                Dic_ElementAll = new ConcurrentDictionary<IODriver, List<Element_IO>>();
                Dic_IsInitialized = new ConcurrentDictionary<IODriver, bool>();

                for (int drvIdx = 0; drvIdx < NoOfDrivers; drvIdx++)
                {
                    string strIODriver = xmlCfgPrm.Get_Item($"Driver_{drvIdx}", "Name");
                    if (Enum.TryParse(strIODriver, out IODriver ioDriver) == false)
                    {
                        // #. IODriver is not defined in Framework
                        return false;
                    }

                    switch (ioDriver)
                    {
                        case IODriver.CREVIS_MODBUS_TCP:
                            if (Dic_ElementAll.ContainsKey(ioDriver) == false)
                            {
                                Dic_ElementAll.TryAdd(ioDriver, new List<Element_IO>());
                            }

                            string ipAddress = xmlCfgPrm.Get_Item($"Driver_{drvIdx}", "IPAddress");
                            int updatePeriod_ms = int.Parse(xmlCfgPrm.Get_Item($"Driver_{drvIdx}", "UpdatePeriod_ms"));

                            Fieldbus_CrevisModbusTCP.ModbusTCPInfo modbusInfo = new Fieldbus_CrevisModbusTCP.ModbusTCPInfo(drvIdx, ipAddress);

                            int NoOfSlots = int.Parse(xmlCfgPrm.Get_Item($"Driver_{drvIdx}", "NoOfSlots"));
                            for (int slotIdx = 0; slotIdx < NoOfSlots; slotIdx++)
                            {
                                string slotName = xmlCfgPrm.Get_Item($"Driver_{drvIdx}", $"Slot_{slotIdx}", "Name");
                                if (slotName == "Reserved")
                                {
                                    continue;
                                }

                                string strSlotType = xmlCfgPrm.Get_Item($"Driver_{drvIdx}", $"Slot_{slotIdx}", "Type");
                                if (Enum.TryParse(strSlotType, out Fieldbus.SlotType slotType) == false)
                                {
                                    // #. SlotType is not defined in Framework
                                    return false;
                                }
                                int sizeOfBits = int.Parse(xmlCfgPrm.Get_Item($"Driver_{drvIdx}", $"Slot_{slotIdx}", "SizeOfBits"));
                                int NoOfChannels = int.Parse(xmlCfgPrm.Get_Item($"Driver_{drvIdx}", $"Slot_{slotIdx}", "NoOfChannels"));

                                Fieldbus_CrevisModbusTCP.SlotInfo slotInfo = new Fieldbus_CrevisModbusTCP.SlotInfo(modbusInfo, slotIdx, slotName, slotType, sizeOfBits, NoOfChannels);

                                for (int chIdx = 0; chIdx < NoOfChannels; chIdx++)
                                {
                                    string strChType = xmlCfgPrm.Get_Item($"Driver_{drvIdx}", $"Slot_{slotIdx}", $"Channel_{chIdx}", "Type");
                                    if (Enum.TryParse(strChType, out IOType chType) == false)
                                    {
                                        // #. IOType is not defined in Framework
                                        return false;
                                    }
                                    if (chType == IOType.Unknown)
                                    {
                                        // #. IOType is not defined in Framework
                                        return false;
                                    }
                                    string section = string.Empty;
                                    string name = string.Empty;
                                    if (chType == IOType.Reserved)
                                    {
                                        section = chType.ToString();
                                        name = section;
                                    }
                                    else
                                    {
                                        section = xmlCfgPrm.Get_Item($"Driver_{drvIdx}", $"Slot_{slotIdx}", $"Channel_{chIdx}", "Section");
                                        name = xmlCfgPrm.Get_Item($"Driver_{drvIdx}", $"Slot_{slotIdx}", $"Channel_{chIdx}", "Name");
                                    }
                                    Element_IO elem = new Element_IO(ioDriver, chType, section, name, slotIdx, chIdx, slotInfo);
                                    Dic_ElementAll[ioDriver].Add(elem);
                                }
                            }
                            break;
                    }
                }
            }

            return true;
        }

        public static bool Load_Config(string xmlCfgFileName)
        {
            if (File.Exists(xmlCfgFileName) == true)
            {
                XmlConfig = (XmlCfg_IO)XmlCfg.Deserialize_XmlCfg(xmlCfgFileName, typeof(XmlCfg_IO));
                int NoOfDrivers = XmlConfig.DriverInfo.DriverCount;
                if (NoOfDrivers > 0)
                {
                    Dic_ElementAll = new ConcurrentDictionary<IODriver, List<Element_IO>>();
                    Dic_IsInitialized = new ConcurrentDictionary<IODriver, bool>();

                    for (int drvCnt = 0; drvCnt < NoOfDrivers; drvCnt++)
                    {
                        IODriver ioDriver = (IODriver)Enum.Parse(typeof(IODriver), XmlConfig.DriverInfo.Driver[drvCnt].Name);
                        switch (ioDriver)
                        {
                            case IODriver.CREVIS_MODBUS_TCP:
                                if (Dic_ElementAll.ContainsKey(ioDriver) == false)
                                {
                                    Dic_ElementAll.TryAdd(ioDriver, new List<Element_IO>());
                                }
                                string ipAddress = XmlConfig.DriverInfo.Driver[drvCnt].IPAddress;
                                int updatePeriod_ms = XmlConfig.DriverInfo.Driver[drvCnt].UpdatePeriod_ms;

                                Fieldbus_CrevisModbusTCP.ModbusTCPInfo modbusInfo = new Fieldbus_CrevisModbusTCP.ModbusTCPInfo(drvCnt, ipAddress);

                                int NoOfSlots = XmlConfig.DriverInfo.Driver[drvCnt].SlotInfo.SlotCount;
                                for (int slotCnt = 0; slotCnt < NoOfSlots; slotCnt++)
                                {
                                    int slotIdx = XmlConfig.DriverInfo.Driver[drvCnt].SlotInfo.Slot[slotCnt].Index;
                                    string slotName = XmlConfig.DriverInfo.Driver[drvCnt].SlotInfo.Slot[slotCnt].Name;
                                    Fieldbus.SlotType slotType = (Fieldbus.SlotType)Enum.Parse(typeof(Fieldbus.SlotType), XmlConfig.DriverInfo.Driver[drvCnt].SlotInfo.Slot[slotIdx].Type);
                                    int sizeOfBits = XmlConfig.DriverInfo.Driver[drvCnt].SlotInfo.Slot[slotCnt].SizeOfBits;
                                    int NoOfChannels = XmlConfig.DriverInfo.Driver[drvCnt].SlotInfo.Slot[slotCnt].ChannelCount;

                                    Fieldbus_CrevisModbusTCP.SlotInfo slotInfo = new Fieldbus_CrevisModbusTCP.SlotInfo(modbusInfo, slotIdx, slotName, slotType, sizeOfBits, NoOfChannels);

                                    for (int chCnt = 0; chCnt < NoOfChannels; chCnt++)
                                    {
                                        try
                                        {
                                            int chIdx = XmlConfig.DriverInfo.Driver[drvCnt].SlotInfo.Slot[slotIdx].Channel[chCnt].Index;
                                            string section = XmlConfig.DriverInfo.Driver[drvCnt].SlotInfo.Slot[slotIdx].Channel[chIdx].Section;
                                            string name = XmlConfig.DriverInfo.Driver[drvCnt].SlotInfo.Slot[slotIdx].Channel[chIdx].Name;
                                            IOType ioType = (IOType)Enum.Parse(typeof(IOType), XmlConfig.DriverInfo.Driver[drvCnt].SlotInfo.Slot[slotIdx].Channel[chIdx].Type);
                                            Element_IO elem = new Element_IO(ioDriver, ioType, section, name, slotIdx, chIdx, slotInfo);
                                            Dic_ElementAll[ioDriver].Add(elem);
                                        }
                                        catch
                                        {
                                            // #. Reserved
                                            Element_IO elem = new Element_IO(ioDriver, IOType.Reserved, "Reserved", $"{slotIdx}-{chCnt}", slotIdx, chCnt, slotInfo);
                                            Dic_ElementAll[ioDriver].Add(elem);
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
                return true;
            }

            return false;
        }

        public static Element_IO Get_Elem(string logicalName)
        {
            if (Dic_ElementsByLogicalName.ContainsKey(logicalName) == true)
            {
                return Dic_ElementsByLogicalName[logicalName];
            }
            return null;
        }

        public static List<Element_IO> Get_ElemAll()
        {
            return Dic_ElementsByLogicalName.Values.ToList();
        }

        public static bool Init_Driver(int ioUpdateInterval_ms = 100)
        {
            Dic_ElementAll.Keys.ToList().ForEach(ioDriver =>
            {
                // Open and Init. Driver
                switch (ioDriver)
                {
                    case IODriver.CREVIS_MODBUS_TCP:
                        if (Fieldbus_CrevisModbusTCP.IsInitialized == false)
                        {
                            Dic_IsInitialized[ioDriver] = Fieldbus_CrevisModbusTCP.Init_CrevisModbusTCP(Dic_ElementAll[ioDriver], ioUpdateInterval_ms);
                        }
                        break;
                }
            });

            return Dic_IsInitialized.Values.Contains(false) == false;
        }

        public static void Enable_UpdateValueObjectContinuously(bool enable, int interval = 0)
        {
            if (enable == true && interval > 0)
            {
                tmr_ReadContinuously.Interval = interval;
                tmr_ReadContinuously.Elapsed += Tmr_UpdateValueObject_Elapsed;
                tmr_ReadContinuously.Enabled = true;
                tmr_ReadContinuously.Start();
            }
            else
            {
                tmr_ReadContinuously.Elapsed -= Tmr_UpdateValueObject_Elapsed;
                tmr_ReadContinuously.Enabled = false;
                tmr_ReadContinuously.Stop();
            }
        }

        private static void Tmr_UpdateValueObject_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dic_ElementAll.Values.ToList().ForEach(driver =>
            {
                driver.ForEach(elem =>
                {
                    elem.UpdateValueObject();
                });
            });
        }


        public object ValueObject { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public readonly IODriver Driver = IODriver.Unknown;
        public readonly IOType Type = IOType.Unknown;
        public readonly string Section = string.Empty;
        public readonly string Name = string.Empty;
        public readonly string LogicalName = string.Empty;
        public readonly int ModuleNo = -1;
        public readonly int ChannelNo = -1;
        public object Setting = null;


        public Element_IO(IODriver ioDriver, IOType type, string section, string name, int moduleIdx, int chIdx, object setting)
        {
            Driver = ioDriver;
            Type = type;
            Section = section;
            Name = name;
            LogicalName = $"{section}-{name}";
            ModuleNo = moduleIdx;
            ChannelNo = chIdx;
            Setting = setting;

            if (Dic_ElementsByLogicalName.TryAdd(LogicalName, this) == false)
            {
                // #. Invalid
            }
        }

        public bool UpdateValueObject()
        {
            (bool getSuccess, object state) rtn = (false, null);
            switch (Type)
            {
                case IOType.DIN:
                    rtn = Get_DigitalIn();
                    break;

                case IOType.DOUT:
                    rtn = Get_DigitalOut();
                    break;

                case IOType.AIN:
                    rtn = Get_AnalogIn();
                    break;

                case IOType.AOUT:
                    rtn = Get_AnalogOut();
                    break;
            }

            NotifyPropertyChanged("ValueObject");
            return rtn.getSuccess;
        }

        public (bool getSuccess, bool state) Get_DigitalIn()
        {
            if (Type != IOType.DIN)
            {
                throw new NotImplementedException($"#. This IO type is {Type}.");
            }

            bool getSuccess = false;
            bool state = false;
            switch (Driver)
            {
                case IODriver.CREVIS_MODBUS_TCP:
                    (getSuccess, state) = Fieldbus_CrevisModbusTCP.Get_DigitalValue(this);
                    if (getSuccess == true)
                    {
                        ValueObject = state;
                    }
                    break;

                default:
                    getSuccess = false;
                    break;
            }
            return (getSuccess, state);
        }

        public (bool getSuccess, bool state) Get_DigitalOut()
        {
            if (Type != IOType.DOUT)
            {
                throw new NotImplementedException($"#. This IO type is {Type}.");
            }

            bool getSuccess = false;
            bool state = false;
            switch (Driver)
            {
                case IODriver.CREVIS_MODBUS_TCP:
                    (getSuccess, state) = Fieldbus_CrevisModbusTCP.Get_DigitalValue(this);
                    if (getSuccess == true)
                    {
                        ValueObject = state;
                    }
                    break;

                default:
                    getSuccess = false;
                    break;
            }
            return (getSuccess, state);
        }

        public bool Set_DigitalOut(bool state, bool sync = false, int syncTimeOut_ms = 500)
        {
            if (Type != IOType.DOUT)
            {
                Log.WriteLog("Error", $"Invalid Operation. LogicalName={LogicalName}, Type={Type}", true);
                throw new NotImplementedException($"#. This IO type is {Type}.");
            }

            bool getSuccess = false;
            switch (Driver)
            {
                case IODriver.CREVIS_MODBUS_TCP:
                    getSuccess = Fieldbus_CrevisModbusTCP.Set_DigitalValue(this, state, sync, syncTimeOut_ms);
                    if (getSuccess == true)
                    {
                        ValueObject = state;
                    }
                    break;

                default:
                    getSuccess = false;
                    break;
            }
            return getSuccess;
        }

        public (bool GetSuccess, AnalogCurrentRange CurrentRange) GetParam_AnalogIn()
        {
            if (Type != IOType.AIN)
            {
                throw new NotImplementedException($"#. This IO type is {Type}.");
            }

            bool bGetSuccess = false;
            AnalogCurrentRange range = AnalogCurrentRange.Unknown;
            switch (Driver)
            {
                case IODriver.CREVIS_MODBUS_TCP:
                    var rtn = Fieldbus_CrevisModbusTCP.GetParam_AnalogIn(this);
                    if (rtn.GetSuccess == true)
                    {
                        range = rtn.CurrentRange;
                        bGetSuccess = true;
                    }
                    break;

                default:
                    bGetSuccess = false;
                    break;
            }
            return (bGetSuccess, range);
        }

        public bool SetParam_AnalogIn(AnalogCurrentRange currentRange)
        {
            if (Type != IOType.AIN)
            {
                throw new NotImplementedException($"#. This IO type is {Type}.");
            }

            bool bSetSuccess = false;
            switch (Driver)
            {
                case IODriver.CREVIS_MODBUS_TCP:
                    var rtn = Fieldbus_CrevisModbusTCP.SetParam_AnalogIn(this, currentRange);
                    if (rtn == true)
                    {
                        bSetSuccess = true;
                    }
                    break;
            }
            return bSetSuccess;
        }

        public (bool GetSuccess, double Value) Get_AnalogIn()
        {
            if (Type != IOType.AIN)
            {
                throw new NotImplementedException($"#. This IO type is {Type}.");
            }
            bool bGetSuccess = false;
            double dValue = 0;
            switch (Driver)
            {
                case IODriver.CREVIS_MODBUS_TCP:
                    Fieldbus_CrevisModbusTCP.SlotInfo slotInfo = (Fieldbus_CrevisModbusTCP.SlotInfo)this.Setting;
                    var rtn = Fieldbus_CrevisModbusTCP.Get_AnalogValue(this);
                    if (rtn.getSuccess == true)
                    {
                        if (slotInfo.SlotName == "GT-3934")
                        {
                            dValue = (16 * (double)rtn.value / 0x7fff + 4);
                            ValueObject = dValue;
                        }
                        else
                        {
                            throw new NotImplementedException($"Slot is not defined ({slotInfo.SlotName}).");
                        }
                        bGetSuccess = true;
                    }
                    break;

                default:
                    bGetSuccess = false;
                    break;
            }
            return (bGetSuccess, dValue);
        }

        public (bool getSuccess, double Value) Get_AnalogOut()
        {
            if (Type != IOType.AOUT)
            {
                throw new NotImplementedException($"#. This IO type is {Type}.");
            }

            bool bGetSuccess = false;
            double dValue = 0;
            switch (Driver)
            {
                case IODriver.CREVIS_MODBUS_TCP:
                    Fieldbus_CrevisModbusTCP.SlotInfo slotInfo = (Fieldbus_CrevisModbusTCP.SlotInfo)this.Setting;
                    var rtn = Fieldbus_CrevisModbusTCP.Get_AnalogValue(this);
                    if (rtn.getSuccess == true)
                    {
                        if (slotInfo.SlotName == "GT-4254")
                        {
                            dValue = (16 * (double)rtn.value / 0x7fff + 4);
                            ValueObject = dValue;
                        }
                        else
                        {
                            throw new NotImplementedException($"Slot is not defined ({slotInfo.SlotName}).");
                        }
                        bGetSuccess = true;
                    }
                    break;

                default:
                    bGetSuccess = false;
                    break;
            }
            return (bGetSuccess, dValue);
        }

        public bool Set_AnalogOut(double dValue)
        {
            if (Type != IOType.AOUT)
            {
                throw new NotImplementedException($"#. This IO type is {Type}.");
            }

            bool setSuccess = false;
            switch (Driver)
            {
                case IODriver.CREVIS_MODBUS_TCP:
                    Fieldbus_CrevisModbusTCP.SlotInfo slotInfo = (Fieldbus_CrevisModbusTCP.SlotInfo)this.Setting;
                    if (slotInfo.SlotName == "GT-4254")
                    {
                        if (dValue < 4)
                        {
                            dValue = 4;
                        }
                        else if (dValue > 20)
                        {
                            dValue = 20;
                        }
                        int nValue = (int)((dValue - 4) * 0x7fff / 16);
                        setSuccess = Fieldbus_CrevisModbusTCP.Set_AnalogValue(this, nValue);
                        if (setSuccess == true)
                        {
                            ValueObject = dValue;
                        }
                    }
                    else
                    {
                        throw new NotImplementedException($"Slot is not defined ({slotInfo.SlotName}).");
                    }
                    break;

                default:
                    setSuccess = false;
                    break;
            }
            return setSuccess;
        }
    }
}
