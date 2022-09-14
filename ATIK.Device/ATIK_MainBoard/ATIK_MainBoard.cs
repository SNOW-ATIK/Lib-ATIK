using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATIK.Device.ATIK_MainBoard
{

    public static class ATIK_MainBoard
    {
        private static Dictionary<DefinedMainBoards, IMB_Driver> MainBoardDrivers = new Dictionary<DefinedMainBoards, IMB_Driver>();

        public static bool Initialize(string cfgFileName)
        {
            // Parse MainBoard Config File
            XmlCfgPrm cfg = new XmlCfgPrm(cfgFileName, "ATIK_MainBoard");
            if (cfg.XmlLoaded == false)
            {
                Log.WriteLog("Error", $"Failed to Load Xml. (FileName={cfgFileName}, RootName=ATIK_MainBoard)");
                return false;
            }

            Array defined = Enum.GetValues(typeof(DefinedMainBoards));
            int noOfBoards = int.Parse(cfg.Get_Item("NoOfBoards"));
            for (int i = 0; i < noOfBoards; i++)
            {
                string boardID = cfg.Get_Item($"Board_{i}", "BoardID");
                if (Enum.TryParse(boardID, out DefinedMainBoards board) == true)
                {
                    string comElemName = cfg.Get_Item($"Board_{i}", "ComElemName");
                    switch (board)
                    {
                        case DefinedMainBoards.Sigma:
                            break;

                        case DefinedMainBoards.L_Titrator:
                            DrvMB_L_Titrator drv_LT = new DrvMB_L_Titrator(comElemName);
                            MainBoardDrivers.Add(board, drv_LT);
                            break;
                    }
                }
            }

            
            return true;
        }

        public static IMB_Driver Get_Driver(DefinedMainBoards boardID)
        {
            if (MainBoardDrivers.ContainsKey(boardID) == false)
            {
                throw new KeyNotFoundException($"{boardID} is not found.");
            }
            return MainBoardDrivers[boardID];
        }

        // Info        
        public static bool IsInitialized(DefinedMainBoards boardID)
        {
            if (MainBoardDrivers.ContainsKey(boardID) == true)
            {
                return MainBoardDrivers[boardID].IsInitialized;
            }
            return false;
        }

        // Drive
        public static void Set_BitState(DefinedMainBoards boardID, int lineOrder, int bitOrder, bool state)
        {
            Get_Driver(boardID).Set_BitState(lineOrder, bitOrder, state);
        }

        public static bool Get_BitState(DefinedMainBoards boardID, int lineOrder, int bitOrder)
        {
            return Get_Driver(boardID).Get_BitState(lineOrder, bitOrder);
        }

        public static void Set_AnalogValueRaw(DefinedMainBoards boardID, int lineOrder, int analogValRaw)
        {
            Get_Driver(boardID).Set_AnalogValueRaw(lineOrder, analogValRaw);
        }

        public static int Get_AnalogValueRaw(DefinedMainBoards boardID, int lineOrder)
        {
            return Get_Driver(boardID).Get_AnalogValueRaw(lineOrder);
        }

        public static bool Run_Syringe(DefinedMainBoards boardID, int lineOrder, MB_SyringeFlow flow, MB_SyringeDirection dir, int vol_Digit, int speed)
        {
            return Get_Driver(boardID).Run_Syringe(lineOrder, flow, dir, vol_Digit, speed);
        }

        public static (bool IsValid, int Volume_Digit) Get_SyringeCurrentPosition(DefinedMainBoards boardID, int lineOrder)
        {
            return Get_Driver(boardID).Get_SyringeCurrentPosition(lineOrder);
        }

        public static (bool IsValid, MB_SyringeDirection PortDirection) Get_SyringePortDirection(DefinedMainBoards boardID, int lineOrder)
        {
            return Get_Driver(boardID).Get_SyringePortDirection(lineOrder);
        }
    }
}
