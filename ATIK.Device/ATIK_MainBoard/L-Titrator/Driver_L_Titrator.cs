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

        public DrvMB_L_Titrator(string comElemName)
        {
            // Load Map
            Map = new MB_Map();

            // Init Com.
            DataHandler = new MB_DataHandler(DefinedMainBoards.L_Titrator.ToString(), comElemName);
        }

        // Protocol
        public MB_Protocol Get_Protocol()
        {
            return DataHandler.Get_Protocol();
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
        public void Set_BitState(int lineOrder, int bitOrder, bool state)
        {
            DataHandler.Set_Bit((LineOrder)lineOrder, bitOrder, state);
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

        public bool Run_Syringe(int lineOrder, MB_SyringeFlow flow, MB_SyringeDirection dir, int vol_Digit, int speed)
        {
            if ((LineOrder)lineOrder != LineOrder.Syringe_1 && (LineOrder)lineOrder != LineOrder.Syringe_2)
            {
                return false;
            }

            string sChNo = (LineOrder)lineOrder == LineOrder.Syringe_1 ? "0" : "1";
            string sFlow = flow == MB_SyringeFlow.Pick ? "0" : "1";
            string sDir = string.Empty;
            switch (dir)
            {
                case MB_SyringeDirection.In:
                    sDir = "1";
                    break;

                case MB_SyringeDirection.Out:
                    sDir = "2";
                    break;

                case MB_SyringeDirection.Ext:
                    sDir = "3";
                    break;
            }
            string sSpeed = speed.ToString("00");
            string sVolume = vol_Digit.ToString("00000");

            string txLine = $"{sChNo}{sFlow}{sDir}{sSpeed}{sVolume}";
            bool setDone = DataHandler.Set_Line((LineOrder)lineOrder, txLine, true);
            bool resetDone = false;
            if (setDone == true)
            {
                txLine = "0000000000";
                resetDone = DataHandler.Set_Line((LineOrder)lineOrder, txLine, true);
            }

            bool bSetSuccess = setDone & resetDone;

            return bSetSuccess;
        }

        public (bool IsValid, int Volume_Digit) Get_SyringeCurrentPosition(int lineOrder)
        {
            LineOrder line = (LineOrder)lineOrder;
            if (line != LineOrder.Syringe_1 && line != LineOrder.Syringe_2)
            {
                return (false, 0);
            }

            string sLine = DataHandler.Get_Line((LineOrder)lineOrder);
            if (sLine.Length < 10)
            {
                return (false, 0);
            }

            /* TBD: Validity check by direction bit?
            bool isValid = int.Parse(sLine.Substring(2, 1)) == 0; // Direction Bit> 0:PositionReturn 1:In, 2:Out, 3:Ext
            if (isValid == true)
            {
                return (true, int.Parse(sLine.Substring(5, 5)));
            }
            */

            string sPos = sLine.Substring(5, 5);
            if (int.TryParse(sPos, out int pos) == true)
            {
                return (true, pos);
            }
            
            return (false, 0);
        }

        public (bool IsValid, MB_SyringeDirection PortDirection) Get_SyringePortDirection(int lineOrder)
        {
            LineOrder line = (LineOrder)lineOrder;
            if (line != LineOrder.Syringe_1 && line != LineOrder.Syringe_2)
            {
                return (false, 0);
            }

            string sLine = DataHandler.Get_Line((LineOrder)lineOrder);
            if (sLine.Length < 10)
            {
                return (false, 0);
            }

            string sDir = sLine.Substring(2, 1);
            if (int.TryParse(sDir, out int dir) == true)
            {
                return (true, (MB_SyringeDirection)dir);
            }
            return (false, 0);
        }
    }
}
