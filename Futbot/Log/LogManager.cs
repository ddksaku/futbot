using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Futbot;
using System.Threading;
using Futbot.Log;

namespace Futbot
{
    public class LogManager
    {
        private static readonly object _lockObject = new object();
        
        private static long _nextWriteTime = DateTime.Now.Ticks;
        
        private static int _flushFrequency;
        private static Logger _log;
        private static int _level = 0;

        public const int DEBUG = 0;
        public const int INFO = 1;
        public const int WARNING = 2;
        public const int ERROR = 3;

        static LogManager()
        {
            _level = Configuration.LogLevel;
            _flushFrequency = Configuration.LogFlushFrequency;

            string logMode = Configuration.LogFileMode;
            switch (logMode)
            {
                case "Sandbox":
                    _log = SandboxLogger.Log;
                    break;
                case "File":
                    _log = new TextLogger(Configuration.LogFilePath);                        
                    break;
                case "Console":
                    _log = new ConsoleLogger();
                    break;
                default:                   
                    break;
            }            
        }

        public static void Debug(string message)
        {
            if (_level > DEBUG) return;

            Write("DEBUG", Thread.CurrentThread.Name, message);
        }

        public static void Info(string message)
        {
            if (_level > INFO) return;

            Write("INFO", Thread.CurrentThread.Name, message);
        }

        public static void Warning(string message)
        {
            if (_level > WARNING) return;

            Write("WARNING", Thread.CurrentThread.Name, message);
        }

        public static void Error(string message)
        {
            if (_level > ERROR) return;

            Write("ERROR", Thread.CurrentThread.Name, message);
        }

        private static void Write(string level, string thread, string message)
        {
            lock (_lockObject)
            {
                _log.Write(String.Format("%s, %s, %s, %s", DateTime.Now.ToString("dd/MM/yy HH:mm:ss"), thread, level, message));

                if (DateTime.Now.Ticks > _nextWriteTime)
                {
                    _log.Flush();
                    _nextWriteTime = DateTime.Now.Ticks + _flushFrequency;
                }
            }
        }
    }
}
