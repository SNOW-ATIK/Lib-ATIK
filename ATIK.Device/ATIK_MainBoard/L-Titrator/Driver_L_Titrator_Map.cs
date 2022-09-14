using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ATIK.Device.ATIK_MainBoard
{
    public partial class DrvMB_L_Titrator
    {
        public class MB_Map
        {
            private BitCtrl_Map Map_Bit;
            private AnalogCtrl_Map Map_Analog;
            private SyringeCtrl_Map Map_Syringe;
            private Dictionary<string, IMB_Elem> AllElems = new Dictionary<string, IMB_Elem>();

            public MB_Map()
            {
                Map_Bit = new BitCtrl_Map(Path.Combine(@"Config\Device\ATIK_MainBoard\L_Titrator", "Map_BitControl.xml"), "Map_BitControl");
                Map_Analog = new AnalogCtrl_Map(Path.Combine(@"Config\Device\ATIK_MainBoard\L_Titrator", "Map_AnalogControl.xml"), "Map_AnalogControl");
                Map_Syringe = new SyringeCtrl_Map(Path.Combine(@"Config\Device\ATIK_MainBoard\L_Titrator", "Map_SyringeControl.xml"), "Map_SyringeControl");                
            }

            public List<MB_Elem_Bit> Get_Bits(LineOrder lineOrder)
            {
                return Map_Bit.Get_Bits(lineOrder);
            }

            public MB_Elem_Bit Get_Bit(LineOrder lineOrder, int bitOrder)
            {
                return Map_Bit.Get_Bit(lineOrder, bitOrder);
            }

            public MB_Elem_Analog Get_Analog(LineOrder lineOrder)
            {
                return Map_Analog.Get_Analog(lineOrder);
            }

            public MB_Elem_Syringe Get_Syringe(LineOrder lineOrder)
            {
                return Map_Syringe.Get_Syringe(lineOrder);
            }
        }

        public class BitCtrl_Map
        {
            public bool IsLoaded { get; private set; }
            private Dictionary<LineOrder, List<MB_Elem_Bit>> BitCtrlGroup = new Dictionary<LineOrder, List<MB_Elem_Bit>>();

            public BitCtrl_Map(string mapFileName, string rootName)
            {
                XmlCfgPrm cfg = new XmlCfgPrm(mapFileName, rootName);
                if (cfg.XmlLoaded == false)
                {
                    Log.WriteLog("Error", $"Failed to Load Xml. (FileName={mapFileName}, RootName={rootName})");
                    IsLoaded = false;
                    return;
                }
                Array LineContents = Enum.GetValues(typeof(LineOrder));
                for (int lineIdx = 0; lineIdx < LineContents.Length; lineIdx++)
                {
                    LineOrder lineOrder = (LineOrder)LineContents.GetValue(lineIdx);
                    string lineName = lineOrder.ToString();
                    MB_IOType ioType = (MB_IOType)Enum.Parse(typeof(MB_IOType), cfg.Get_Item(lineName, "IOType"));
                    int nBitLengthInLine = int.Parse(cfg.Get_Item(lineName, "NoOfBits"));
                    if (nBitLengthInLine > 0)
                    {
                        List<MB_Elem_Bit> elems = new List<MB_Elem_Bit>();
                        for (int bitIdx = 0; bitIdx < nBitLengthInLine; bitIdx++)
                        {
                            string logicalName = cfg.Get_Item(lineName, $"Bit_{bitIdx}", "Name");
                            bool errorWhenOnState = bool.Parse(cfg.Get_Item(lineName, $"Bit_{bitIdx}", "ErrorWhenOnState"));
                            MB_Elem_Bit elem = new MB_Elem_Bit(DefinedMainBoards.L_Titrator, logicalName, (int)lineOrder, ioType, bitIdx, errorWhenOnState);
                            elems.Add(elem);
                        }
                        BitCtrlGroup.Add(lineOrder, elems);
                    }
                }
                IsLoaded = BitCtrlGroup.Count > 0;
            }

            public List<MB_Elem_Bit> Get_Bits(LineOrder lineOrder)
            {
                if (BitCtrlGroup.ContainsKey(lineOrder) == true)
                {
                    return BitCtrlGroup[lineOrder];
                }
                return null;
            }

            public MB_Elem_Bit Get_Bit(LineOrder lineOrder, int bitOrder)
            {
                if (BitCtrlGroup.ContainsKey(lineOrder) == true)
                {
                    return BitCtrlGroup[lineOrder][bitOrder];
                }
                return null;
            }
        }

        public class AnalogCtrl_Map
        {
            public bool IsLoaded { get; private set; }
            private Dictionary<LineOrder, MB_Elem_Analog> AnalogCtrlGroup = new Dictionary<LineOrder, MB_Elem_Analog>();

            public AnalogCtrl_Map(string mapFileName, string rootName)
            {
                XmlCfgPrm cfg = new XmlCfgPrm(mapFileName, rootName);
                if (cfg.XmlLoaded == false)
                {
                    Log.WriteLog("Error", $"Failed to Load Xml. (FileName={mapFileName}, RootName={rootName})");
                    IsLoaded = false;
                    return;
                }
                Array LineContents = Enum.GetValues(typeof(LineOrder));
                for (int lineIdx = 0; lineIdx < LineContents.Length; lineIdx++)
                {
                    LineOrder lineOrder = (LineOrder)LineContents.GetValue(lineIdx);
                    string lineName = lineOrder.ToString();
                    string logicalName = cfg.Get_Item(lineName, "Name");
                    MB_IOType ioType = (MB_IOType)Enum.Parse(typeof(MB_IOType), cfg.Get_Item(lineName, "IOType"));
                    string unit = cfg.Get_Item(lineName, "Unit");
                    double scaleFactor = double.Parse(cfg.Get_Item(lineName, "ScaleFactor"));
                    MB_Elem_Analog elem = new MB_Elem_Analog(DefinedMainBoards.L_Titrator, logicalName, (int)lineOrder, ioType, unit, scaleFactor);
                    AnalogCtrlGroup.Add(lineOrder, elem);
                }
                IsLoaded = AnalogCtrlGroup.Count > 0;
            }

            public MB_Elem_Analog Get_Analog(LineOrder lineOrder)
            {
                if (AnalogCtrlGroup.ContainsKey(lineOrder) == true)
                {
                    return AnalogCtrlGroup[lineOrder];
                }
                return null;
            }
        }

        public class SyringeCtrl_Map
        {
            public bool IsLoaded { get; private set; }
            private Dictionary<LineOrder, MB_Elem_Syringe> SyringeCtrlMap = new Dictionary<LineOrder, MB_Elem_Syringe>();

            public SyringeCtrl_Map(string mapFileName, string rootName)
            {
                XmlCfgPrm cfg = new XmlCfgPrm(mapFileName, rootName);
                if (cfg.XmlLoaded == false)
                {
                    Log.WriteLog("Error", $"Failed to Load Xml. (FileName={mapFileName}, RootName={rootName})");
                    IsLoaded = false;
                    return;
                }
                Array LineContents = Enum.GetValues(typeof(LineOrder));
                for (int lineIdx = 0; lineIdx < LineContents.Length; lineIdx++)
                {
                    LineOrder lineOrder = (LineOrder)LineContents.GetValue(lineIdx);
                    if (lineOrder != LineOrder.Syringe_1 && lineOrder != LineOrder.Syringe_2)
                    {
                        continue;
                    }
                    string lineName = lineOrder.ToString();
                    string logicalName = cfg.Get_Item(lineName, "Name");
                    bool extEnabled = bool.Parse(cfg.Get_Item(lineName, "Ext_Enabled"));
                    int maxSpeed = int.Parse(cfg.Get_Item(lineName, "MaxSpeed"));
                    int maxVolume = int.Parse(cfg.Get_Item(lineName, "MaxVolume"));
                    double scaleFactor = double.Parse(cfg.Get_Item(lineName, "ScaleFactor"));
                    MB_Elem_Syringe elem = new MB_Elem_Syringe(DefinedMainBoards.L_Titrator, logicalName, (int)lineOrder, extEnabled, maxVolume, maxSpeed, scaleFactor);
                    SyringeCtrlMap.Add(lineOrder, elem);
                }
                IsLoaded = SyringeCtrlMap.Count > 0;
            }

            public MB_Elem_Syringe Get_Syringe(LineOrder lineOrder)
            {
                if (SyringeCtrlMap.ContainsKey(lineOrder) == true)
                {
                    return SyringeCtrlMap[lineOrder];
                }
                return null;
            }
        }
    }
}
