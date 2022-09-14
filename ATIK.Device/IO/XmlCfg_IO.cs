// TBD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ATIK.Device.IO
{
    [Serializable]
    [XmlRoot("XmlCfg_IO")]
    public class XmlCfg_IO
    {
        [XmlElement("DriverInfo")]
        public DriverInfo DriverInfo { get; set; }
    }

    [Serializable]
    public class DriverInfo
    {
        [XmlElement("NoOfDrivers")]
        public int DriverCount { get; set; }

        [XmlArray("DriverList")]
        [XmlArrayItem("Driver", typeof(Driver))]
        public Driver[] Driver;
    }


    [Serializable]
    public class Driver
    {
        [XmlElement("Index")]
        public int Index { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Type")]
        public string Type { get; set; }

        [XmlElement("Comport")]
        public string Comport { get; set; }

        [XmlElement("IPAddress")]
        public string IPAddress { get; set; }

        [XmlElement("PortNo")]
        public int PortNo { get; set; }

        [XmlElement("UpdatePeriod_ms")]
        public int UpdatePeriod_ms { get; set; }

        [XmlElement("SlotInfo")]
        public SlotInfo SlotInfo { get; set; }
    }

    [Serializable]
    public class SlotInfo
    {
        [XmlElement("NoOfSlots")]
        public int SlotCount { get; set; }

        [XmlArray("SlotList")]
        [XmlArrayItem("Slot", typeof(Slot))]
        public Slot[] Slot;
    }

    [Serializable]
    public class Slot
    {
        [XmlElement("Index")]
        public int Index { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Type")]
        public string Type { get; set; }

        [XmlElement("SizeOfBits")]
        public int SizeOfBits { get; set; }

        [XmlElement("NoOfChannels")]
        public int ChannelCount { get; set; }

        [XmlArray("ChannelList")]
        [XmlArrayItem("Channel", typeof(Channel))]
        public Channel[] Channel;

    }

    [Serializable]
    public class Channel
    {
        [XmlElement("Index")]
        public int Index { get; set; }

        [XmlElement("Type")]
        public string Type { get; set; }

        [XmlElement("Section")]
        public string Section { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }
    }
}
