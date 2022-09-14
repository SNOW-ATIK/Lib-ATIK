using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ATIK.Communication.SerialPort
{
    [Serializable]
    [XmlRoot("XmlCfg_SerialPort")]
    public class XmlCfg_SerialPort
    {
        [XmlElement("NoOfSerialPorts")]
        public int NoOfSerialPorts { get; set; }

        [XmlArray("SerialPortList")]
        [XmlArrayItem("SerialPort", typeof(SerialPort))]
        public SerialPort[] SerialPort;
    }

    [Serializable]
    public class SerialPort
    {
        [XmlElement("Section")]
        public string Section { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        public Setting Setting { get; set; }
    }

    [Serializable]
    public class Setting
    {
        [XmlElement("PortName")]
        public string PortName { get; set; }

        [XmlElement("BaudRate")]
        public int BaudRate { get; set; }

        [XmlElement("Parity")]
        public string Parity { get; set; }

        [XmlElement("DataBits")]
        public int DataBits { get; set; }

        [XmlElement("StopBits")]
        public int StopBits { get; set; }

        [XmlElement("ReadTimeOut")]
        public int ReadTimeOut { get; set; }

        [XmlElement("WriteTimeOut")]
        public int WriteTimeOut { get; set; }

        [XmlElement("Terminator")]
        public string Terminator { get; set; }
    }
}
