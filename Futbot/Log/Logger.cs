using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Futbot.Log
{
    public abstract class Logger
    {
        protected object _objectlock = new object();

        public void Write(string message)
        {
            lock (_objectlock)
            {
                DoWrite(message);
            }
        }

        public void Flush()
        {
            lock (_objectlock)
            {
                DoFlush();
            }
        }

        protected abstract void DoWrite(string message);

        protected abstract void DoFlush();
    }

    public class TextLogger : Logger
    {
        private TextWriter _log;

        public TextLogger(string file)
        {
            _log = TextWriter.Synchronized(File.AppendText(Configuration.LogFilePath));
        }

        protected override void DoWrite(string message)
        {
            _log.WriteLine(message);
        }

        protected override void DoFlush()
        {
            _log.Flush();
        }
    }

    public class ConsoleLogger : Logger
    {
        protected override void DoWrite(string message)
        {
            Console.WriteLine(message);
        }

        protected override void DoFlush()
        {
            // NA
        }
    }

    public class SandboxLogger : Logger
    {
        private static SandboxLogger _activeLog;

        public static SandboxLogger Log
        {
            get { return _activeLog ?? new SandboxLogger(); }
        }

        private StringBuilder _log;
        private Stream _output;        
        private string _logText { get; set; }        
        
        public SandboxLogger()
        {
            _log = new StringBuilder();
        }

        protected override void DoWrite(string message)
        {
            _log.AppendLine(message);
        }

        protected override void DoFlush()
        {
            _logText = _log.ToString();
            _log.Clear();
        }

        public string GetText()
        {
            string ret = String.Empty;
            lock (_objectlock)
            {
                ret = _logText;
                _logText = String.Empty;
            }
            return ret;
        }
    }
}
