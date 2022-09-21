using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATIK.Device.ATIK_MainBoard
{

    public class MB_Elem_Bit : IMB_Elem
    {
        private static Dictionary<string, MB_Elem_Bit> ElemsAll = new Dictionary<string, MB_Elem_Bit>();

        private DefinedMainBoards _BoardID;
        private int _LineNo;
        private MB_IOType _IOType;

        public DefinedMainBoards BoardID { get => _BoardID; }
        public int LineNo { get => _LineNo; }
        public MB_ElemType ElemType { get => MB_ElemType.Bit; }
        public MB_IOType IOType { get => _IOType; }

        public string LogicalName { get; private set; }
        public int BitOrder { get; private set; }
        public bool ErrorWhenOnState { get; private set; }

        public MB_Elem_Bit(DefinedMainBoards boardID, string logicalName, int lineNo, MB_IOType ioType, int bitOrder, bool errorWhenOnState)
        {
            _BoardID = boardID;
            LogicalName = logicalName;
            _LineNo = lineNo;
            _IOType = ioType;

            BitOrder = bitOrder;
            ErrorWhenOnState = errorWhenOnState;

            if (ElemsAll.ContainsKey(LogicalName) == false)
            {
                ElemsAll.Add(LogicalName, this);
            }
        }

        public void Set_State(bool state)
        {
            ATIK_MainBoard.Set_BitState(BoardID, LineNo, BitOrder, state);
        }

        public bool Get_State()
        {
            return ATIK_MainBoard.Get_BitState(BoardID, LineNo, BitOrder);
        }

        public static MB_Elem_Bit GetElem(string logicalName)
        {
            if (ElemsAll.ContainsKey(logicalName) == true)
            {
                return ElemsAll[logicalName];
            }
            return null;
        }
    }

    public class MB_Elem_Analog : IMB_Elem
    {
        private static Dictionary<string, MB_Elem_Analog> ElemsAll = new Dictionary<string, MB_Elem_Analog>();

        private DefinedMainBoards _BoardID;
        private int _LineNo;
        private MB_IOType _IOType;

        public DefinedMainBoards BoardID { get => _BoardID; }
        public int LineNo { get => _LineNo; }
        public MB_ElemType ElemType { get => MB_ElemType.Analog; }
        public MB_IOType IOType { get => _IOType; }

        public string LogicalName { get; private set; }
        public string Unit { get; private set; }
        public double ScaleFactor { get; private set; }

        public MB_Elem_Analog(DefinedMainBoards boardID, string logicalName, int lineNo, MB_IOType ioType, string unit, double scaleFactor)
        {
            _BoardID = boardID;
            LogicalName = logicalName;
            _LineNo = lineNo;
            _IOType = ioType;

            Unit = unit;
            ScaleFactor = scaleFactor;

            if (ElemsAll.ContainsKey(LogicalName) == false)
            {
                ElemsAll.Add(LogicalName, this);
            }
        }

        public void Set_Value(double value)
        {
            int digitValue = (int)(value / ScaleFactor);
            ATIK_MainBoard.Set_AnalogValueRaw(BoardID, LineNo, digitValue);
        }

        public double Get_Value()
        {
            int digitVal = ATIK_MainBoard.Get_AnalogValueRaw(BoardID, LineNo);
            return digitVal * ScaleFactor;
        }

        public static MB_Elem_Analog GetElem(string logicalName)
        {
            if (ElemsAll.ContainsKey(logicalName) == true)
            {
                return ElemsAll[logicalName];
            }
            return null;
        }
    }

    public class MB_Elem_Syringe : IMB_Elem
    {
        public enum RunCmdStatus
        {
            Checking,
            Done,
        }

        private static Dictionary<string, MB_Elem_Syringe> ElemsAll = new Dictionary<string, MB_Elem_Syringe>();

        private DefinedMainBoards _BoardID;
        private int _LineNo;
        private MB_IOType _IOType = MB_IOType.Output;
        private MB_SyringeDirection _PortDirection = MB_SyringeDirection.None;

        public DefinedMainBoards BoardID { get => _BoardID; }
        public int LineNo { get => _LineNo; }
        public MB_ElemType ElemType { get => MB_ElemType.Syringe; }
        public MB_IOType IOType { get => _IOType; }
        public MB_SyringeDirection PortDirection { get => _PortDirection; }

        public string LogicalName { get; private set; }
        public bool ExtDirection_Enabled { get; private set; }
        public int MaxVolume_Raw { get; private set; }
        public int MaxSpeed { get; private set; }
        public double MaxVolume_mL { get => MaxVolume_Raw / ScaleFactor; }
        public double ScaleFactor { get; private set; }
        public double PositionEndBandwidth { get; private set; }

        private System.Threading.Thread RunSyringe;

        public bool RunCmdSuccess { get; private set; }
        public RunCmdStatus RunCmdDone
        {
            get
            {
                if (RunSyringe == null || RunSyringe.IsAlive == false)
                {
                    return RunCmdStatus.Done;
                }
                else
                {
                    return RunCmdStatus.Checking;
                }
            }
        }

        public MB_Elem_Syringe(DefinedMainBoards boardID, string logicalName, int lineNo, bool extEnabled, int maxVolume, int maxSpeed, double scaleFactor, double posEndBandwidth)
        {
            _BoardID = boardID;
            LogicalName = logicalName;
            _LineNo = lineNo;

            ExtDirection_Enabled = extEnabled;
            MaxVolume_Raw = maxVolume;
            MaxSpeed = maxSpeed;
            ScaleFactor = scaleFactor;
            PositionEndBandwidth = posEndBandwidth;

            if (ElemsAll.ContainsKey(LogicalName) == false)
            {
                ElemsAll.Add(LogicalName, this);
            }
        }

        public bool Run_Raw(MB_SyringeFlow flow, MB_SyringeDirection direction, int volume_Raw, int speed, bool sync = true)
        {
            RunCmdSuccess = false;
            RunSyringe = new System.Threading.Thread(RunSyringeProc) { IsBackground = true };
            RunSyringe.Start(new object[] { flow, direction, volume_Raw, speed });
            if (sync == true)
            {
                RunSyringe.Join();
            }
            return true;
        }

        public bool Run_mL(MB_SyringeFlow flow, MB_SyringeDirection direction,  double volume_mL, int speed, bool sync = true)
        {
            RunCmdSuccess = false;
            int volume_Digit = (int)(volume_mL * ScaleFactor);
            RunSyringe = new System.Threading.Thread(RunSyringeProc) { IsBackground = true };
            RunSyringe.Start(new object[] { flow, direction, volume_Digit, speed });
            if (sync == true)
            {
                RunSyringe.Join();
            }
            return true;
        }

        private void RunSyringeProc(object runInfo)
        {
            object[] info = (object[])runInfo;
            RunCmdSuccess = ATIK_MainBoard.Run_Syringe(BoardID, LineNo, (MB_SyringeFlow)info[0], (MB_SyringeDirection)info[1], (int)info[2], (int)info[3]);
        }

        public MB_SyringeDirection Get_PortDirection()
        {
            var DirRtn = ATIK_MainBoard.Get_SyringePortDirection(BoardID, LineNo);
            if (DirRtn.IsValid == true)
            {
                return DirRtn.PortDirection;
            }
            return MB_SyringeDirection.None;
        }

        public (bool IsValid, int Volume_Raw) Get_Volume_Raw()
        {
            var PositionRtn = ATIK_MainBoard.Get_SyringeCurrentPosition(BoardID, LineNo);
            if (PositionRtn.IsValid == true)
            {
                int pos_Raw = PositionRtn.Volume_Digit;
                return (true, pos_Raw);
            }
            return (false, 0);
        }

        public (bool IsValid, double Volume_mL) Get_Volume_mL()
        {
            var PositionRtn = ATIK_MainBoard.Get_SyringeCurrentPosition(BoardID, LineNo);
            if (PositionRtn.IsValid == true)
            {
                double pos_mL = PositionRtn.Volume_Digit / ScaleFactor;
                return (true, pos_mL);
            }
            return (false, 0);
        }

        public static MB_Elem_Syringe GetElem(string logicalName)
        {
            if (ElemsAll.ContainsKey(logicalName) == true)
            {
                return ElemsAll[logicalName];
            }
            return null;
        }
    }
}
