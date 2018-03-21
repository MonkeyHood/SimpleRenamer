using System;
using System.Diagnostics;

namespace SimpleRenamer
{
    public enum LogLevel
    {
        Info,
        Warning
    }

    public static class Logger
    {
        public static void Log(LogLevel logLevel, string msg, params object[] args)
        {
            switch (logLevel)
            {
                case LogLevel.Info:
                    LogInfo(msg, args);
                    break;
                case LogLevel.Warning:
                    LogWarning(msg, args);
                    break;
            }
        }

        [Conditional("DEBUG")]
        private static void LogInfo(string msg, object[] args)
        {
            InternalLog(msg, args);
        }

        [Conditional("WARNING")]
        private static void LogWarning(string msg, object[] args)
        {
            InternalLog(msg, args);
        }

        private  static void InternalLog(string msg, params object[] args)
        {
            Console.WriteLine(msg, args);
        }
    }
}
