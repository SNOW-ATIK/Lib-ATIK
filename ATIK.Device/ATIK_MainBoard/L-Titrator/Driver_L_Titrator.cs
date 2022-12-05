using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATIK.Device.ATIK_MainBoard
{
    public partial class DrvMB_L_Titrator : IMB_Driver
    {
        public bool IsInitialized 
        {
            get
            {
                if (DataHandler == null)
                {
                    return false;
                }
                return DataHandler.IsLoaded & DataHandler.IsOpened;
            }
        }

        public bool ComStatus
        {
            get
            {
                if (DataHandler == null)
                {
                    return false;
                }
                return DataHandler.ComStatus;
            }
        }

        private MB_Map Map;
        private MB_DataHandler DataHandler;
        private Dictionary<LineOrder, LockedBool> SyringeSendingState = new Dictionary<LineOrder, LockedBool>();

        public DrvMB_L_Titrator(string comElemName, bool interlockEnabled = true)
        {
            // Load Map
            Map = new MB_Map();

            // Init Com.
            DataHandler = new MB_DataHandler(DefinedMainBoards.L_Titrator.ToString(), comElemName, interlockEnabled);

            SyringeSendingState.Clear();
            SyringeSendingState.Add(LineOrder.Syringe_1, new LockedBool());
            SyringeSendingState.Add(LineOrder.Syringe_2, new LockedBool());
        }

        // Protocol
        public MB_Protocol Get_Protocol()
        {
            return DataHandler.Get_Protocol();
        }

        public void Enable_Interlock(bool enable)
        {
            DataHandler.Enable_Interlock(enable);
        }

        // Tx, Rx Frame
        public List<string> Get_TxFrame()
        {
            return DataHandler.Get_TxFrame();
        }

        public List<string> Get_RxFrame()
        {
            return DataHandler.Get_RxFrame();
        }

        // Map
        public List<MB_Elem_Bit> Get_Bits(int lineNo)
        {
            return Map.Get_Bits((LineOrder)lineNo);
        }

        public MB_Elem_Bit Get_Bit(int lineNo, int bitOrder)
        {
            return Map.Get_Bit((LineOrder)lineNo, bitOrder);
        }

        public MB_Elem_Analog Get_Analog(int lineNo)
        {
            return Map.Get_Analog((LineOrder)lineNo);
        }

        public MB_Elem_Syringe Get_Syringe(int lineNo)
        {
            return Map.Get_Syringe((LineOrder)lineNo);
        }

        // Drive
        public void Set_BitState(int lineNo, int bitOrder, bool state)
        {
            DataHandler.Set_Bit((LineOrder)lineNo, bitOrder, state);
        }

        public bool Get_BitState(int lineOrder, int bitOrder)
        {
            return DataHandler.Get_Bit((LineOrder)lineOrder, bitOrder);
        }

        public void Set_AnalogValueRaw(int lineOrder, int valueRaw)
        {
            string sValue = valueRaw.ToString();
            int properLength = DataHandler.Get_DefinedLineLength(lineOrder);
            sValue = sValue.PadLeft(properLength, '0');

            DataHandler.Set_Line((LineOrder)lineOrder, sValue);
        }

        public int Get_AnalogValueRaw(int lineOrder)
        {
            string line = DataHandler.Get_Line((LineOrder)lineOrder);
            if (int.TryParse(line, out int rtn) == true)
            {
                return rtn;
            }
            return 0;
        }

        public bool Init_Syringe(int lineNo)
        {
            LineOrder lineOrder = (LineOrder)lineNo;
            if (lineOrder != LineOrder.Syringe_1 && lineOrder != LineOrder.Syringe_2)
            {
                return false;
            }
            string sChNo = lineOrder == LineOrder.Syringe_1 ? "0" : "1";
            string sErr = "0";
            string sWrite = ((int)MB_SyringeRW.Write).ToString("0");
            string sCmd = ((int)MB_SyringeCommand.Initialize).ToString("0");
            string txLine = $"{sChNo}{sErr}{sWrite}{sCmd}000000000";

            bool setDone = DataHandler.Write_Syringe((LineOrder)lineNo, txLine);

            return setDone;
        }

        public bool Run_Syringe(int lineNo, MB_SyringeFlow flow, MB_SyringeDirection dir, int vol_Digit, int speed)
        {
            LineOrder lineOrder = (LineOrder)lineNo;
            if (lineOrder != LineOrder.Syringe_1 && lineOrder != LineOrder.Syringe_2)
            {
                return false;
            }

            string sChNo = lineOrder == LineOrder.Syringe_1 ? "0" : "1";
            string sErr = "0";
            string sWrite = ((int)MB_SyringeRW.Write).ToString("0");
            string sCmd = ((int)MB_SyringeCommand.Move).ToString("0");
            string sFlow = ((int)flow).ToString("0");
            string sDir = ((int)dir).ToString("0");
            string sSpeed = speed.ToString("00");
            string sVolume = vol_Digit.ToString("00000");

            string txLine = $"{sChNo}{sErr}{sWrite}{sCmd}{sFlow}{sDir}{sSpeed}{sVolume}";

            // Protocol이 변경되면서, F/W에서는 항상 현재 위치 정보를 리턴한다.
            // TBD. 더이상 보내는 중이라는 Flag가 필요하지 않을 것으로 예상되니, 추후 확인하여 필요하면 삭제토록 한다.
            SyringeSendingState[lineOrder].Value = true;
            bool setDone = DataHandler.Write_Syringe((LineOrder)lineNo, txLine);
            SyringeSendingState[lineOrder].Value = false;

            return setDone;
        }

        public (bool IsValid, MB_SyringeStatus Status) Get_SyringeStatus(int lineNo)
        {
            LineOrder lineOrder = (LineOrder)lineNo;
            if (lineOrder != LineOrder.Syringe_1 && lineOrder != LineOrder.Syringe_2)
            {
                return (false, MB_SyringeStatus.Error);
            }

            // TBD. 삭제 검토
            if (SyringeSendingState[lineOrder].Value == true)
            {
                return (false, MB_SyringeStatus.Error);
            }

            string sLine = DataHandler.Get_Line(lineOrder);
            if (sLine.Length != (int)MB_SyringePacketStruct.Length)
            {
                return (false, MB_SyringeStatus.Error);
            }

            string sStatus = sLine.Substring((int)MB_SyringePacketStruct.Command_Status, 1);
            if (int.TryParse(sStatus, out int status) == true)
            {
                return (true, (MB_SyringeStatus)status);
            }
            return (false, MB_SyringeStatus.Error);
        }

        public (bool IsValid, int Volume_Digit) Get_SyringeCurrentPosition(int lineNo)
        {
            LineOrder lineOrder = (LineOrder)lineNo;
            if (lineOrder != LineOrder.Syringe_1 && lineOrder != LineOrder.Syringe_2)
            {
                return (false, 0);
            }

            // TBD. 삭제 검토
            if (SyringeSendingState[lineOrder].Value == true)
            {
                return (false, 0);
            }

            string sLine = DataHandler.Get_Line(lineOrder);
            if (sLine.Length != (int)MB_SyringePacketStruct.Length)
            {
                return (false, 0);
            }

            string sPos = sLine.Substring((int)MB_SyringePacketStruct.Distance_Position, 5);
            if (int.TryParse(sPos, out int pos) == true)
            {
                return (true, pos);
            }
            
            return (false, 0);
        }

        public (bool IsValid, MB_SyringeDirection PortDirection) Get_SyringePortDirection(int lineNo)
        {
            LineOrder lineOrder = (LineOrder)lineNo;
            if (lineOrder != LineOrder.Syringe_1 && lineOrder != LineOrder.Syringe_2)
            {
                return (false, 0);
            }

            // TBD. 삭제 검토
            if (SyringeSendingState[lineOrder].Value == true)
            {
                return (false, 0);
            }

            string sLine = DataHandler.Get_Line(lineOrder);
            if (sLine.Length != (int)MB_SyringePacketStruct.Length)
            {
                return (false, 0);
            }

            string sDir = sLine.Substring((int)MB_SyringePacketStruct.In_Out_Ext, 1);
            if (int.TryParse(sDir, out int dir) == true)
            {
                return (true, (MB_SyringeDirection)dir);
            }
            return (false, 0);
        }
    }

    public class LockedBool
    {
        private object LockObject = new object();
        private bool _LockedValue = false;
        public bool Value
        {
            get
            {
                lock (LockObject)
                {
                    return _LockedValue;
                }
            }
            set
            {
                lock (LockObject)
                {
                    _LockedValue = value;
                }
            }
        }
    }
}
