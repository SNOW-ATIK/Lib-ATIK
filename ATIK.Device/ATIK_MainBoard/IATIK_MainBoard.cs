using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATIK.Device.ATIK_MainBoard
{
    // Interfaces
    public interface IATIK_MainBoard
    {
    }

    public interface IMB_Elem
    {
        DefinedMainBoards BoardID { get; }
        int LineNo { get; }
        MB_ElemType ElemType { get; }
        MB_IOType IOType { get; }
    }

    public interface IMB_Driver
    {
        bool IsInitialized { get; }
        
        MB_Protocol Get_Protocol();

        List<string> Get_TxFrame();

        List<string> Get_RxFrame();

        List<MB_Elem_Bit> Get_Bits(int lineNo);

        MB_Elem_Bit Get_Bit(int lineNo, int bitOrder);

        MB_Elem_Analog Get_Analog(int lineNo);

        MB_Elem_Syringe Get_Syringe(int lineNo);

        void Set_BitState(int lineNo, int bitOrder, bool state);
        bool Get_BitState(int lineNo, int bitOrder);

        void Set_AnalogValueRaw(int lineNo, int analogValRaw);
        int Get_AnalogValueRaw(int lineNo);

        bool Run_Syringe(int lineOrder, MB_SyringeFlow flow, MB_SyringeDirection dir, int vol_Digit, int speed);
        (bool IsValid, int Volume_Digit) Get_SyringeCurrentPosition(int lineOrder);
        (bool IsValid, MB_SyringeDirection PortDirection) Get_SyringePortDirection(int lineOrder);
    }

    // Enums
    public enum DefinedMainBoards
    {
        None,
        Sigma,
        L_Titrator,
    }

    public enum MB_ElemType
    {
        Bit,
        Analog,
        Syringe,
    }

    public enum MB_IOType
    {
        Input,
        Output,
    }

    public enum MB_SyringeFlow
    {
        Pick,
        Dispense,
        None
    }

    public enum MB_SyringeDirection
    {
        In,
        Out,
        Ext,
        None
    }

    // class
    public class Protocol_ID
    {
        public readonly string PC_to_Board;
        public readonly string Board_to_PC;

        public Protocol_ID(string pc, string board)
        {
            PC_to_Board = pc;
            Board_to_PC = board;
        }
    }

    public class LineContents
    {
        public readonly string Name;
        public readonly int Length;

        public LineContents(string name, int length)
        {
            Name = name;
            Length = length;
        }
    }

    public class MB_Protocol
    {
        public bool IsLoaded { get; private set; }
        public bool IsValid { get; private set; }
        public int TotalLines { get; private set; }

        private Protocol_ID Protocol_ID;
        private List<LineContents> LineContents = new List<LineContents>();

        public MB_Protocol(string protocolCfgFileName, string rootName)
        {
            XmlCfgPrm cfg = new XmlCfgPrm(protocolCfgFileName, rootName);
            IsLoaded = cfg.XmlLoaded;
            if (IsLoaded == false)
            {
                return;
            }

            Protocol_ID = new Protocol_ID(cfg.Get_Item("Protocol_ID", "PC_to_Board"), cfg.Get_Item("Protocol_ID", "Board_to_PC"));

            TotalLines = int.Parse(cfg.Get_Item("LineDef", "TotalLines"));
            for (int i = 0; i < TotalLines; i++)
            {
                LineContents line = new LineContents(cfg.Get_Item("LineDef", $"Line_{i}", "Name"), int.Parse(cfg.Get_Item("LineDef", $"Line_{i}", "Length")));
                LineContents.Add(line);
            }
        }

        public string Get_Protocol_ID_PC()
        {
            return Protocol_ID.PC_to_Board;
        }

        public string Get_Protocol_ID_Board()
        {
            return Protocol_ID.Board_to_PC;
        }

        public List<LineContents> Get_LineContentsAll()
        {
            return LineContents;
        }

        public string Get_LineName(int lineOrder)
        {
            if (lineOrder < LineContents.Count)
            {
                return LineContents[lineOrder].Name;
            }
            return "Invalid";
        }

        public int Get_LineLength(int lineOrder)
        {
            if (lineOrder < LineContents.Count)
            {
                return LineContents[lineOrder].Length;
            }
            return 0;
        }
    }
}
