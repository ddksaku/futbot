using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Futbot
{
    public class Configuration
    {        
        public static int LogLevel { get { return GetInt(ConfigurationKeys.APPSETTING_LOG_LEVEL, 4); } }
        public static int LogFlushFrequency { get { return GetInt(ConfigurationKeys.APPSETTING_LOG_FLUSH_FREQUENCY, 100000); } }
        public static string LogFilePath { get { return GetString(ConfigurationKeys.APPSETTING_LOG_FILE_PATH, null); } }
        public static string LogFileMode { get { return GetString(ConfigurationKeys.APPSETTING_LOG_FILE_MODE, null); } }

        public static string GetString(string key, string defaultValue)
        {
            return ConfigurationManager.AppSettings[key] ?? defaultValue;
        }

        public static int GetInt(string key, int defaultValue)
        {
            try
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings[key]);
            }
            catch
            {
                return defaultValue;
            }
        }
    }

    public class ConfigurationKeys
    {
        public const string APPSETTING_LOG_LEVEL = "LogLevel";
        public const string APPSETTING_LOG_FILE_PATH = "LogFilePath";
        public const string APPSETTING_LOG_FILE_MODE = "LogFileMode";
        public const string APPSETTING_LOG_FLUSH_FREQUENCY = "LogFlushFrequency";
    }
}
