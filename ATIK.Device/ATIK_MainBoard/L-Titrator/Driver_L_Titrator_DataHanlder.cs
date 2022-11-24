using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace ATIK.Device.ATIK_MainBoard
{
    public partial class DrvMB_L_Titrator
    {
        public class MB_DataHandler
        {
            public string LogicalName { get; private set; }
            public bool IsLoaded { get; private set; }
            public bool IsOpened { get; private set; }
            public bool IsInterlockEnabled { get; private set; }

            private object objLock_ComStatus = new object();
            private bool _ComStatus = false;
            public bool ComStatus
            {
                get
                {
                    lock (objLock_ComStatus)
                    {
                        return _ComStatus;
                    }
                }
                set
                {
                    lock (objLock_ComStatus)
                    {
                        _ComStatus = value;
                    }
                }
            }

            private MB_Protocol Protocol;
            private ATIK_MB_ComHandler ComHandler;

            private ConcurrentDictionary<LineOrder, string> TxFrame = new ConcurrentDictionary<LineOrder, string>();

            public MB_DataHandler(string boardID, string comElemName, bool interlockEnabled = true)
            {
                LogicalName = $"{boardID}-{comElemName}";
                IsInterlockEnabled = interlockEnabled;

                // Load Protocol
                Protocol = new MB_Protocol($@"Config\Device\ATIK_MainBoard\{boardID}\Protocol.xml", "Protocol");
                if (Protocol.IsLoaded == false)
                {
                    Log.WriteLog("Error", $"Failed to Load Protocol. (BoardID={boardID}, RootName=Protocol)");
                    IsLoaded = false;
                    return;
                }
                var lineContents = Protocol.Get_LineContentsAll();

                // Check validity between xml and enum
                bool isValid = true;
                Array arrEnum = Enum.GetValues(typeof(LineOrder));
                for (int i = 0; i < Protocol.TotalLines; i++)
                {
                    if (lineContents[i].Name != ((LineOrder)arrEnum.GetValue(i)).ToString())
                    {
                        isValid = false;
                        break;
                    }
                }
                IsLoaded = isValid;

                // Init. Com.
                ComHandler = new ATIK_MB_ComHandler(LogicalName);
                if (ComHandler.IsOpened == false)
                {
                    IsOpened = false;
                    return;
                }
                IsOpened = ComHandler.IsOpened;

                // Init. Frames
                Init_TxFrame();

                ComHandler.DataReceivedEvent += ComHandler_DataReceivedEvent;
                ComHandler.ComErrorEvent += ComHandler_ComErrorEvent;
                ComHandler.PassInterlockEvent += ComHandler_InterlockPassEvent;
                ComHandler.RedirectTxPacketEvent += ComHandler_RedirectTxPacketEvent;

                Set_Frame(TxFrame.Values.ToList());

                Stopwatch st = new Stopwatch();
                st.Start();
                while (Get_RxFrame().Count == 0)
                {
                    if (st.ElapsedMilliseconds > 1000)
                    {
                        // #. Receive TimeOut
                        break;
                    }
                    Thread.Sleep(100);
                }
            }

            public void Enable_Interlock(bool enable)
            {
                IsInterlockEnabled = enable;
            }

            private bool ComHandler_InterlockPassEvent()
            {
                if (IsInterlockEnabled == true)
                {
                    bool bLeak = Get_Bit(LineOrder.Alarm_Input, 0) == false;     // Leak_1(Leak)'s BitOrder is 0.
                    if (bLeak == true)
                    {
                        Log.WriteLog("Error", $"Check Leak.", true);
                    }

                    bool bOverFlow = Get_Bit(LineOrder.Alarm_Input, 3) == false; // Lever_2(OverFlow)'s BitOrder is 3.
                    if (bOverFlow == true)
                    {
                        Log.WriteLog("Error", $"Check OverFlow.", true);
                    }

                    if (bLeak == true || bOverFlow == true)
                    {
                        return false;
                    }
                }
                return true;
            }

            private void ComHandler_RedirectTxPacketEvent()
            {
                // Close DIW_To_6Way
                Set_Bit(LineOrder.Solenoid_Output, 0, false);

                // Close DIW_To_Vessel
                Set_Bit(LineOrder.Solenoid_Output, 1, false);

                // Close Slurry_To_3Way
                Set_Bit(LineOrder.Solenoid_Output, 2, false);

                // Close Ceric_To_3Way
                Set_Bit(LineOrder.Solenoid_Output, 3, false);
            }

            private void ComHandler_DataReceivedEvent(List<string> recvData)
            {
                int nLines = Enum.GetValues(typeof(LineOrder)).Length;
                if (recvData.Count != nLines)
                {
                    // #. Invalid Format
                    Log.WriteLog("Error", $"Line Count is not matched. (recv={recvData.Count}, collections={nLines})");
                    ComStatus = false;
                    return;
                }

                ComStatus = true;
            }

            private void ComHandler_ComErrorEvent()
            {
                ComStatus = false;
            }

            // TBD. Valve, Syringe, Analog Output 등의 초기 상태를 정해야 한다.
            private void Init_TxFrame()
            {
                Array lineDef = Enum.GetValues(typeof(LineOrder));
                for (int i = 0; i < lineDef.Length; i++)
                {
                    LineOrder line = (LineOrder)lineDef.GetValue(i);
                    switch (line)
                    {
                        case LineOrder.STX:
                            TxFrame.TryAdd(line, ATIK_MB_ComHandler.STX);
                            break;

                        case LineOrder.Protocol_ID:
                            TxFrame.TryAdd(line, Protocol.Get_Protocol_ID_PC());
                            break;

                        case LineOrder.ETX:
                            TxFrame.TryAdd(line, ATIK_MB_ComHandler.ETX);
                            break;

                        default:
                            string temp = "";
                            temp = temp.PadLeft(Protocol.Get_LineLength(i), '0');
                            TxFrame.TryAdd((LineOrder)lineDef.GetValue(i), temp);
                            break;
                    }
                }
            }

            public void Set_Frame(List<string> frame)
            {
                ComHandler.Set_TxFrame(frame);
            }

            public List<string> Get_TxFrame()
            {
                return ComHandler.Get_TxFrame();
            }

            public List<string> Get_RxFrame()
            {
                return ComHandler.Get_RxFrame();
            }

            public bool Set_Line(LineOrder lineOrder, string txLine, bool sync = false)
            {
                ComHandler.Set_TxLine((int)lineOrder, txLine);
                if (sync == true)
                {
                    bool sameAsTx = false;
                    Stopwatch st = new Stopwatch();
                    st.Start();
                    while (st.ElapsedMilliseconds < 1000)
                    {
                        sameAsTx = txLine == ComHandler.Get_RxLine((int)lineOrder);
                        if (sameAsTx == true)
                        {
                            break;
                        }
                        Thread.Sleep(100);
                    }
                    st.Stop();
                    return sameAsTx;
                }
                return true;
            }

            public string Get_Line(LineOrder lineOrder)
            {
                return ComHandler.Get_RxLine((int)lineOrder);
            }

            public void Set_Bit(LineOrder lineOrder, int bitOrder, bool state)
            {
                ComHandler.Set_TxBitState((int)lineOrder, bitOrder, state);
            }

            public bool Get_Bit(LineOrder lineOrder, int bitOrder)
            {
                return ComHandler.Get_RxBitState((int)lineOrder, bitOrder);
            }

            public MB_Protocol Get_Protocol()
            {
                return Protocol;
            }

            public int Get_DefinedLineLength(int lineOrder)
            {
                return Protocol.Get_LineLength(lineOrder);
            }
        }
    }
}
