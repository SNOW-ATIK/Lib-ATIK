using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace ATIK
{
    public static class Log
    {
        private static bool _bInit = false;
        private static object objLock_bInit = new object();
        private static bool bInit
        {
            get
            {
                lock (objLock_bInit)
                {
                    return _bInit;
                }
            }
            set
            {
                lock (objLock_bInit)
                {
                    _bInit = value;
                }
            }
        }

        private static ConcurrentDictionary<string, Logger> DicLogger = new ConcurrentDictionary<string, Logger>();
        private static Thread thrCheckDateChange;

        public delegate void WriteLogEventHandler(string msg);
        public static WriteLogEventHandler WriteLogEvent;

        private static string LogPath = string.Empty;
        private static DateTime LogCreated;

        private static object objLock_RenewLogPath = new object();

        public static void Init_Log(string logPath, object logName, int sizeLimitOfLogfile_mb = 100)
        {
            LogPath = logPath;

            string year = DateTime.Now.Year.ToString("D2");
            string month = DateTime.Now.Month.ToString("D2");
            string day = DateTime.Now.Day.ToString("D2");
            string filePath = $@"{LogPath}\{year}\{month}\{day}";
            if (Directory.Exists(filePath) == false)
            {
                Directory.CreateDirectory(filePath);
                Thread.Sleep(100);
            }

            LogCreated = DateTime.Now;

            if (bInit == false)
            {
                thrCheckDateChange = new Thread(CheckDateChangeProcess) { IsBackground = true };
                thrCheckDateChange.Start();
            }

            string sLogName = logName.ToString();
            DicLogger.TryAdd(sLogName, new Logger(filePath, sLogName, sizeLimitOfLogfile_mb));

            bInit = true;
        }

        public static void Close_Log(string logName)
        {
            if (DicLogger.ContainsKey(logName) == false)
            {
                return;
            }
            DicLogger[logName].Dispose();
        }

        public static void WriteLog(object section, string logMsg, bool display = false)
        {
            string sSection = section.ToString();
            if (DicLogger.ContainsKey(sSection) == false)
            {
                logMsg = $"#. {sSection} Log is not initialized. (LogMsg={logMsg})";
                sSection = "Error";
            }
            DicLogger[sSection].WriteLog(logMsg, display);
        }

        private static void CheckDateChangeProcess()
        {
            while (true)
            {
                if (DateTime.Now.Date.Day != LogCreated.Date.Day)
                {
                    string year = DateTime.Now.Year.ToString("D2");
                    string month = DateTime.Now.Month.ToString("D2");
                    string day = DateTime.Now.Day.ToString("D2");
                    string filePath = $@"{LogPath}\{year}\{month}\{day}";

                    Directory.CreateDirectory(filePath);

                    DicLogger.Values.ToList().ForEach(logger => logger.RenewPatch(filePath));

                    LogCreated = DateTime.Now;
                }

                Thread.Sleep(1000);
            }
        }

        private class Logger
        {
            private string Section = string.Empty;
            private StreamWriter LogWriter;
            private ConcurrentQueue<(string TimeStamp, string Log, bool FlagDisplay)> qLog = new ConcurrentQueue<(string, string, bool)>();
            private Thread thrLogging;
            private (string TimeStamp, string Log) PreviousLogInfo = (string.Empty, string.Empty);
            private int DuplicatedCounts = 0;

            public Logger(string logPath, string section, int sizeLimit_mb)
            {
                Section = section;

                string fileName = $"{Section}_{DateTime.Now:yyyyMMddHHmmss}.log";
                LogWriter = new StreamWriter($@"{logPath}\{fileName}", append: true);

                thrLogging = new Thread(LoggingProcess) { IsBackground = true };
                thrLogging.Start();
            }

            public void WriteLog(string msg, bool display)
            {
                string timeStamp = $"[{DateTime.Now:HH:mm:ss.fff}]";

                qLog.Enqueue((timeStamp, msg, display));
            }

            public void Dispose()
            {
                if (LogWriter != null)
                {
                    LogWriter.Close();
                    LogWriter.Dispose();
                }
                if (thrLogging != null && thrLogging.IsAlive == true)
                {
                    thrLogging.Abort();
                }
            }

            private void LoggingProcess()
            {
                while (true)
                {
                    if (qLog.TryDequeue(out (string TimeStamp, string CurrentLog, bool FlagDisplay) Info) == true)
                    {
                        string writeLogMsg = $"{Info.TimeStamp} {Info.CurrentLog}";
                        if (Info.CurrentLog == PreviousLogInfo.Log)
                        {
                            if (DuplicatedCounts < 10)
                            {
                                ++DuplicatedCounts;
                            }
                            else
                            {
                                writeLogMsg = $"{Info.TimeStamp} {Info.CurrentLog} ... {DuplicatedCounts}";
                                WriteFile(writeLogMsg);

                                if (Info.FlagDisplay == true)
                                {
                                    WriteLogEvent?.Invoke(writeLogMsg);
                                }

                                DuplicatedCounts = 0;
                            }
                        }
                        else
                        {
                            if (DuplicatedCounts > 0)
                            {
                                // Previous
                                string writePrevious = $"{PreviousLogInfo.TimeStamp} {PreviousLogInfo.Log} ... {DuplicatedCounts}";
                                WriteFile(writePrevious);

                                if (Info.FlagDisplay == true)
                                {
                                    WriteLogEvent?.Invoke(writePrevious);
                                }

                                DuplicatedCounts = 0;
                            }
                            WriteFile(writeLogMsg);

                            if (Info.FlagDisplay == true)
                            {
                                WriteLogEvent?.Invoke(writeLogMsg);
                            }
                        }
                        PreviousLogInfo.TimeStamp = Info.TimeStamp;
                        PreviousLogInfo.Log = Info.CurrentLog;

                        continue;
                    }
                    Thread.Sleep(1);
                }
            }

            public void RenewPatch(string logPath)
            {
                lock (objLock_RenewLogPath)
                {
                    string fileName = $"{Section}_{DateTime.Now:yyyyMMddHHmmss}.log";
                    LogWriter.Close();
                    LogWriter.Dispose();

                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    LogWriter = new StreamWriter($@"{logPath}\{fileName}", append: true);
                }
            }

            private void WriteFile(string log)
            {
                lock (objLock_RenewLogPath)
                {
                    LogWriter.WriteLine(log);
                    LogWriter.Flush();
                }
            }
        }
    }
}
