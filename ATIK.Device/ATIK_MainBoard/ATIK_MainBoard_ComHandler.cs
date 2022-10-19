using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ATIK.Communication.SerialPort;

namespace ATIK.Device.ATIK_MainBoard
{
    public class ATIK_MB_ComHandler
    {
        private Element_SerialPort ComElem;
        public const string STX = "02h";
        public const string ETX = "03h";
        public const long TX_INTERVAL = 500;

        public bool IsOpened { get; private set; }

        // Tx
        // IO Frame
        private Thread TxThread;
        private ConcurrentDictionary<int, string> TxPacket = new ConcurrentDictionary<int, string>();
        private ConcurrentQueue<(int, string)> TxQueue = new ConcurrentQueue<(int, string)>();

        // Rx
        public delegate void DataReceivedDelegate(List<string> msg);
        public event DataReceivedDelegate DataReceivedEvent;

        private ConcurrentQueue<string> qReceive = new ConcurrentQueue<string>();
        private Thread thrReceive;

        // ComCheck
        public delegate void ComErrorDelegate();
        public event ComErrorDelegate ComErrorEvent;

        // Frame
        private object objLock_FrameReceiveStart = new object();
        private bool _FrameReceiveStart = false;
        public bool FrameReceiveStart
        {
            get
            {
                lock (objLock_FrameReceiveStart)
                {
                    return _FrameReceiveStart;
                }
            }
            private set
            {
                lock (objLock_FrameReceiveStart)
                {
                    _FrameReceiveStart = value;
                }
            }
        }

        private System.Timers.Timer tmr_ReceiveTimeCheck = new System.Timers.Timer();
        private ConcurrentDictionary<int, string> RxPacket = new ConcurrentDictionary<int, string>();

        private ManualResetEventSlim mrstDataReceived = new ManualResetEventSlim(true);

        // Debug Mode
        private object objLock_BoardDebug = new object();
        private bool _BoardDebug = false;
        public bool BoardDebug
        {
            get
            {
                lock (objLock_BoardDebug)
                {
                    return _BoardDebug;
                }
            }
            set
            {
                lock (objLock_BoardDebug)
                {
                    _BoardDebug = value;
                }
            }
        }

        public ATIK_MB_ComHandler(string logicalName, string logPath = "")
        {
            ComElem = Element_SerialPort.Get_Elem(logicalName);
            if (ComElem != null)
            {
                IsOpened = ComElem.Open();
                if (IsOpened == true)
                {
                    Start_ComThread();
                }
                else
                {
                    Log.WriteLog("Error", $"#. Failed to open. (PortName={ComElem.ComSetting.PortName})");
                }
            }
            else            
            {
                Log.WriteLog("Error", $"#. Failed to get ComElem. (LogicalName={logicalName})");
            }
        }

        private void Start_ComThread()
        {
            TxThread = new Thread(TxProcess) { IsBackground = true };
            TxThread.Start();

            tmr_ReceiveTimeCheck.Enabled = false;
            tmr_ReceiveTimeCheck.AutoReset = true;
            tmr_ReceiveTimeCheck.Interval = 5000;
            tmr_ReceiveTimeCheck.Elapsed += Tmr_ReceiveTimeCheck_Elapsed;

            thrReceive = new Thread(DataReceivedProcess) { IsBackground = true };
            thrReceive.Start();

            ComElem.DataReceivedEvent += Comport_DataReceivedEvent;
        }

        public void Set_TxFrame(List<string> frame)
        {
            TxPacket = new ConcurrentDictionary<int, string>();
            for (int i = 0; i < frame.Count; i++)
            {
                TxPacket.TryAdd(i, frame[i]);
            }
        }

        public bool Set_TxLine(int lineOrder, string line)
        {
            if (TxPacket.ContainsKey(lineOrder) == false)
            {
                Log.WriteLog("Error", $"#. Invalid TxPacket. (TxPacket={TxPacket.Count}, LineOrder={lineOrder}, Msg={line}");
                return false;
            }
            TxPacket[lineOrder] = line;

            return true;
        }

        public bool Set_TxBitState(int lineOrder, int bitOrder, bool state)
        {
            if (TxPacket.ContainsKey(lineOrder) == false)
            {
                return false;
            }
            string CurLine = TxPacket[lineOrder];

            // Reverse
            char[] tmp = CurLine.ToCharArray();
            Array.Reverse(tmp);
            CurLine = new string(tmp);
            CurLine = CurLine.Remove(bitOrder, 1);
            CurLine = CurLine.Insert(bitOrder, Convert.ToInt32(state).ToString());

            // Reverse again
            tmp = CurLine.ToCharArray();
            Array.Reverse(tmp);
            CurLine = new string(tmp);

            return Set_TxLine(lineOrder, CurLine);
        }

        public string Get_RxLine(int lineOrder)
        {
            if (RxPacket.ContainsKey(lineOrder) == false)
            {
                return "";
            }
            return RxPacket[lineOrder];
        }

        public bool Get_RxBitState(int lineOrder, int bitOrder)
        {
            if (RxPacket.ContainsKey(lineOrder) == false)
            {
                return false;
            }
            string CurLine = RxPacket[lineOrder];
            char[] tmp = CurLine.ToCharArray();
            Array.Reverse(tmp);
            CurLine = new string(tmp);
            bool state = Convert.ToBoolean(int.Parse(CurLine.Substring(bitOrder, 1)));

            return state;
        }

        private void TxProcess()
        {
            System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
            while (true)
            {
                if (BoardDebug == true)
                {
                    Thread.Sleep(100);
                    continue;
                }
                if (TxPacket.Count == 0)
                {
                    Thread.Sleep(100);
                    continue;
                }
                bool bSendAll = false;
                int nRetry = 0;
                while (bSendAll == false)
                {
                    st.Reset();
                    st.Start();

                    int nSend = 0;
                    mrstDataReceived.Reset();

                    for (int i = 0; i < TxPacket.Count; i++)
                    {
                        bool sent = ComElem.Send(TxPacket[i]);
                        if (sent == true)
                        {
                            //ATIK.Log.WriteLog(ComElem.ComSetting.PortName, $"Tx> {TxPacket[i]}");
                            ++nSend;
                        }
                        else
                        {
                            // 강제로 ETX 송신
                            bool sendETX = false;
                            while (sendETX == false)
                            {
                                sendETX = ComElem.Send(ETX);
                                Thread.Sleep(100);
                            }
                            break;
                        }
                    }

                    bSendAll = nSend == TxPacket.Count;
                    if (bSendAll == true)
                    {
                        if (mrstDataReceived.Wait((int)(TX_INTERVAL * 3)) == true)
                        {
                            // 정상 송신.

                            st.Stop();
                            int extra = (int)(TX_INTERVAL - st.ElapsedMilliseconds);
                            if (extra >= 0)
                            {
                                Thread.Sleep(extra);
                            }
                        }
                        else
                        {
                            // 정상 송신했으나, 응답 지연 혹은 응답 없음.
                            Log.WriteLog("Debug", $"#. Data is not received in {TX_INTERVAL * 3}ms", true);

                            ComErrorEvent?.Invoke();

                            bSendAll = false;
                            ++nRetry;
                        }
                    }
                    else
                    {
                        // 데이터 송신 실패.
                        ComErrorEvent?.Invoke();

                        ++nRetry;

                        Log.WriteLog("Debug", $"#. Send Fail. (Retry={nRetry})", true);
                        Thread.Sleep((int)TX_INTERVAL);
                    }
                }
            }
        }

        // Rx
        private void Comport_DataReceivedEvent(string msg)
        {
            qReceive.Enqueue(msg);
        }

        private int LineReceiveCount = 0;
        public void DataReceivedProcess()
        {
            while (true)
            {
                if (qReceive.TryDequeue(out string msg) == true)
                {
                    string log = msg;
                    log = log.Replace("\n", "<LF>");
                    log = log.Replace("\r", "<CR>");
                    //ATIK.Log.WriteLog(ComElem.ComSetting.PortName, $"Tx> {log}");

                    msg = msg.Trim();

                    if (msg == STX)
                    {
                        if (RxPacket.Count == 0)
                        {
                            RxPacket.TryAdd(LineReceiveCount, msg);
                        }
                        else
                        {
                            RxPacket[LineReceiveCount] = msg;
                        }
                        ++LineReceiveCount;

                        PacketReceive_Start();
                    }
                    else
                    {
                        // IO Frame
                        if (FrameReceiveStart == true)
                        {
                            if (RxPacket.Keys.Contains(LineReceiveCount) == false)
                            {
                                RxPacket.TryAdd(LineReceiveCount, msg);
                            }
                            else
                            {
                                RxPacket[LineReceiveCount] = msg;
                            }

                            if (msg == ETX)
                            {
                                mrstDataReceived.Set();

                                LineReceiveCount = 0;

                                PacketReceive_End();

                                DataReceivedEvent?.Invoke(RxPacket.Values.ToList());
                            }
                            else
                            {
                                ++LineReceiveCount;
                            }
                        }
                        // Debug Msg
                        else
                        {
                        }
                    }

                    continue;
                }
                Thread.Sleep(1);
            }
        }

        private void Tmr_ReceiveTimeCheck_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            FrameReceiveStart = false;
        }

        public void PacketReceive_Start()
        {
            if (tmr_ReceiveTimeCheck.Enabled == false)
            {
                tmr_ReceiveTimeCheck.Enabled = true;
                tmr_ReceiveTimeCheck.Start();

                FrameReceiveStart = true;
            }
        }

        public void PacketReceive_End()
        {
            if (tmr_ReceiveTimeCheck.Enabled == true)
            {
                tmr_ReceiveTimeCheck.Stop();
                tmr_ReceiveTimeCheck.Enabled = false;

                FrameReceiveStart = false;
            }
        }

        public List<string> Get_TxFrame()
        {
            return TxPacket.Values.ToList();
        }

        public List<string> Get_RxFrame()
        {
            return RxPacket.Values.ToList();
        }
    }
}
