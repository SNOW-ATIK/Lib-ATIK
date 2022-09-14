using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using System.Text.RegularExpressions;

namespace ATIK.Communication.SerialPort
{
    public class Element_SerialPort
    {
        public class Setting
        {
            public string PortName;
            public int BaudRate;
            public Parity Parity;
            public int DataBits;
            public StopBits StopBits;
            public int WriteTimeOut;
            public int ReadTimeOut;
            public string Terminator;
        }

        private static XmlCfg_SerialPort XmlConfig;
        private static ConcurrentDictionary<string, Element_SerialPort> Dic_ElementsAll;
        private static ConcurrentDictionary<string, Element_SerialPort> Dic_ElementsByLogicalName;

        public static bool Load_Config(string xmlCfgFilePath, string xmlCfgFileName, string xmlRootName, string logPath)
        {
            string fileName = Path.Combine(xmlCfgFilePath, xmlCfgFileName);
            if (File.Exists(fileName) == false)
            {
                Log.WriteLog("Error", $"No File. (FileName={fileName})");
                return false;
            }

            if (string.IsNullOrEmpty(xmlRootName) == true)
            {
                xmlRootName = "XmlCfg_SerialPort";
            }
            XmlCfgPrm xmlCfgPrm = new XmlCfgPrm(xmlCfgFilePath, xmlCfgFileName, xmlRootName);
            if (xmlCfgPrm == null)
            {
                Log.WriteLog("Error", $"Failed to Load Xml. (FileName={fileName}, RootName={xmlRootName})");
                return false;
            }

            int NoOfSerialPorts = int.Parse(xmlCfgPrm.Get_Item("NoOfSerialPorts"));
            if (NoOfSerialPorts > 0)
            {
                Dic_ElementsAll = new ConcurrentDictionary<string, Element_SerialPort>();
                Dic_ElementsByLogicalName = new ConcurrentDictionary<string, Element_SerialPort>();

                for (int serialCnt = 0; serialCnt < NoOfSerialPorts; serialCnt++)
                {
                    string section = xmlCfgPrm.Get_Item($"SerialPort_{serialCnt}", "Section");
                    string name = xmlCfgPrm.Get_Item($"SerialPort_{serialCnt}", "Name");
                    string portName = xmlCfgPrm.Get_Item($"SerialPort_{serialCnt}", "Setting", "PortName");
                    int baudRate = int.Parse(xmlCfgPrm.Get_Item($"SerialPort_{serialCnt}", "Setting", "BaudRate"));
                    if (Enum.TryParse(xmlCfgPrm.Get_Item($"SerialPort_{serialCnt}", "Setting", "Parity"), out Parity parity) == false)
                    {
                        parity = Parity.None;
                    }
                    int dataBits = int.Parse(xmlCfgPrm.Get_Item($"SerialPort_{serialCnt}", "Setting", "DataBits"));
                    if (Enum.TryParse(xmlCfgPrm.Get_Item($"SerialPort_{serialCnt}", "Setting", "StopBits"), out StopBits stopBits) == false)
                    {
                        stopBits = StopBits.One;
                    }
                    int writeTimeOut = int.Parse(xmlCfgPrm.Get_Item($"SerialPort_{serialCnt}", "Setting", "WriteTimeOut"));
                    int readTimeOut = int.Parse(xmlCfgPrm.Get_Item($"SerialPort_{serialCnt}", "Setting", "ReadTimeOut"));
                    string terminator = xmlCfgPrm.Get_Item($"SerialPort_{serialCnt}", "Setting", "Terminator");

                    Setting setting = new Setting()
                    {
                        PortName = portName,
                        BaudRate = baudRate,
                        Parity = parity,
                        DataBits = dataBits,
                        StopBits = stopBits,
                        WriteTimeOut = writeTimeOut,
                        ReadTimeOut = readTimeOut,
                        Terminator = terminator
                    };

                    Element_SerialPort elem = new Element_SerialPort(section, name, setting);
                    if (Dic_ElementsAll.ContainsKey(portName) == false)
                    {
                        Dic_ElementsAll.TryAdd(portName, elem);
                    }

                    string logicalName = $"{section}-{name}";
                    if (Dic_ElementsByLogicalName.ContainsKey(logicalName) == false)
                    {
                        Dic_ElementsByLogicalName.TryAdd(logicalName, elem);
                    }

                    Log.Init_Log(logPath, $"{elem.LogKey}");
                    Log.WriteLog(elem.LogKey, $"Element is Created. [{elem.ComSetting.PortName}]");
                }
            }
            return true;
        }

        public static Element_SerialPort Get_Elem(string logicalName)
        {
            if (Dic_ElementsByLogicalName.ContainsKey(logicalName) == true)
            {
                return Dic_ElementsByLogicalName[logicalName];
            }
            return null;
        }

        public static List<Element_SerialPort> Get_ElemsBySection(string section)
        {
            List<Element_SerialPort> lst = new List<Element_SerialPort>();
            Dic_ElementsByLogicalName.Keys.ToList().ForEach(key =>
            {
                string onlySectionName = key.Split('-')[0];
                if (onlySectionName.Equals(section) == true)
                {
                    lst.Add(Dic_ElementsByLogicalName[key]);
                }
            });
            return lst;
        }

        public static List<Element_SerialPort> Get_ElemAll()
        {
            return Dic_ElementsAll.Values.ToList();
        }

        public static string[] GetPortList()
        {
            return System.IO.Ports.SerialPort.GetPortNames();
        }

        public readonly string Section;
        public readonly string Name;
        public readonly string LogicalName;
        public readonly string TerminatorString;
        public readonly string[] TerminatorStringArray;

        public delegate void DataReceived(string msg);
        public event DataReceived DataReceivedEvent;

        public Setting ComSetting;
        private System.IO.Ports.SerialPort Comport;

        public string LogKey { get; private set; }

        public bool IsOpened
        {
            get
            {
                if (Comport != null)
                {
                    return Comport.IsOpen;
                }
                return false;
            }
        }

        public Element_SerialPort(string section, string name, Setting setting, bool initLog = false, string logPath = "")
        {
            Section = section;
            Name = name;
            LogicalName = $"{Section}-{Name}";

            LogKey = $"{LogicalName}-{setting.PortName}";

            ComSetting = setting;

            string chrTerminator = setting.Terminator;
            chrTerminator = chrTerminator.Replace("[CR]", "\r");
            chrTerminator = chrTerminator.Replace("[LF]", "\n");

            TerminatorString = chrTerminator;
            TerminatorStringArray = new string[TerminatorString.Length];
            for (int i = 0; i < TerminatorStringArray.Length; i++)
            {
                TerminatorStringArray[i] = TerminatorString.Substring(i, 1);
            }

            Comport = new System.IO.Ports.SerialPort();
            Comport.PortName = ComSetting.PortName;
            Comport.BaudRate = ComSetting.BaudRate;
            Comport.DataBits = ComSetting.DataBits;
            Comport.StopBits = ComSetting.StopBits;
            Comport.ReadTimeout = ComSetting.ReadTimeOut;
            Comport.WriteTimeout = ComSetting.WriteTimeOut;
            Comport.NewLine = TerminatorString;
        }

        public bool Open()
        {
            if (Comport.IsOpen == true)
            {
                return true;
            }

            bool bOpened = false;
            try
            {
                Comport.Open();
                Comport.DataReceived += SerialDataReceived;
                Comport.ErrorReceived += Comport_ErrorReceived;
                bOpened = true;

                Log.WriteLog(LogKey, $"[{ComSetting.PortName}] Port is Opened.", true);
            }
            catch (Exception e)
            {
                Log.WriteLog(LogKey, $"#. Exception. Msg={e.Message}.", true);
                Comport.DataReceived -= SerialDataReceived;
            }
            return bOpened;
        }

        private void Comport_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            Log.WriteLog(LogKey, $"[{ComSetting.PortName}] #. ErrorReceived. Msg={e.EventType}.", true);
        }

        public void Close()
        {
            if (Comport.IsOpen == true)
            {
                Comport.DataReceived -= SerialDataReceived;
                Comport.Close();

                Log.WriteLog(LogKey, $"[{ComSetting.PortName}] Port is Closed.", true);
            }
        }

        public bool Send(string msg)
        {
            if (Comport.IsOpen == true)
            {
                try
                {
                    Comport.WriteLine(msg);

                    string log = msg;
                    log = log.Replace("\r", "<CR>");
                    log = log.Replace("\n", "<LF>");
                    Log.WriteLog(LogKey, $"[Tx] {log}", false);
                    
                    return true;
                }
                catch
                { 
                }
            }
            return false;
        }

        public bool Send(byte[] data)
        {
            if (Comport.IsOpen == true)
            {
                try
                {
                    Comport.Write(data, 0, data.Length);

                    Log.WriteLog(LogKey, $"[Tx(Byte)] {Util.ByteArrayToString(data)}", false);

                    return true;
                }
                catch
                {
                }
            }
            return false;
        }

        private string Remained = string.Empty;
        private void SerialDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //if (DataReceivedEvent != null)
            //{
            //    try
            //    {
            //        string data = Comport.ReadLine();
            //        if (string.IsNullOrEmpty(data) == false)
            //        {
            //            string log = data;
            //            log = log.Replace("\r", "<CR>");
            //            log = log.Replace("\n", "<LF>");
            //            Log.WriteLog(LogKey, $"[Rx] {log}", false);

            //            DataReceivedEvent?.Invoke(data);
            //        }
            //    }
            //    catch
            //    { 
            //    }
            //}
            if (DataReceivedEvent != null)
            {
                try
                {
                    string received = Comport.ReadExisting();
                    string log = received.Replace("\r", "<CR>");
                    log = log.Replace("\n", "<LF>");
                    //Log.WriteLog(LogKey, $"[Rx] {log}", false);

                    Remained += received;
                    MatchCollection matches = Regex.Matches(Remained, TerminatorString);
                    if (matches.Count > 0)
                    {
                        char[] TerminatorCharacterArray = new char[TerminatorStringArray.Length];
                        if (TerminatorStringArray.Length > 0)
                        {
                            for (int i = 0; i < TerminatorStringArray.Length; i++)
                            {
                                TerminatorCharacterArray[i] = Convert.ToChar(TerminatorStringArray[i]);
                            }
                        }
                        string[] split = Remained.Split(TerminatorCharacterArray);
                        if (Remained.Substring(Remained.Length - 2, 2) == TerminatorString)
                        {
                            for (int i = 0; i < split.Length; i++)
                            {
                                if (split[i] != "")
                                {
                                    DataReceivedEvent?.Invoke(split[i]);
                                    log = split[i];
                                    Log.WriteLog(LogKey, $"[Rx] {log}", false);
                                }
                            }

                            Remained = string.Empty;
                        }
                        else
                        {
                            for (int i = 0; i < split.Length - 1; i++)
                            {
                                if (split[i] != "")
                                {
                                    DataReceivedEvent?.Invoke(split[i]);
                                    log = split[i];
                                    Log.WriteLog(LogKey, $"[Rx] {log}", false);
                                }
                            }

                            Remained = split[split.Length - 1];
                            //Log.WriteLog(LogKey, $"[Rx] (Remained={Remained})", false);
                        }
                        
                    }                    
                }
                catch (Exception exc)
                {
                    Log.WriteLog(LogKey, $"#. Exception. ({exc.Message})", false);
                }
            }
        }
    }
}
