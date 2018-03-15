using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRenamer
{
    public static class Logger
    {
        public static bool EnableLogs;

        public static void Log(string msg, params object[] args)
        {
            Console.WriteLine(msg, args);
        }
    }
}
