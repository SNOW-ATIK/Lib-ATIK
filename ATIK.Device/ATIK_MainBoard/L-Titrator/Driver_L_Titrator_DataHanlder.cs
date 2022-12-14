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

            private MB_Protocol Protocol;
            private ATIK_MB_ComHandler ComHandler;

            private ConcurrentDictionary<LineOrder, string> TxFrame = new ConcurrentDictionary<LineOrder, string>();

            public MB_DataHandler(string boardID, string comElemName)
            {
                LogicalName = $"{boardID}-{comElemName}";
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

            private void ComHandler_DataReceivedEvent(List<string> recvData)
            {
                int nLines = Enum.GetValues(typeof(LineOrder)).Length + 1;
                if (recvData.Count != nLines)
                {
                    return;
                }

                ParsingRxData(recvData);
            }

            private void ParsingRxData(List<string> data)
            {
                if (data[0] != ATIK_MB_ComHandler.STX || data[data.Count - 1] != ATIK_MB_ComHandler.ETX)
                {
                    // #. Invalid STX or ETX
                    return;
                }

                // TBD. Necessary?
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
