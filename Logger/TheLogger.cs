using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Diagnostics;

namespace LoggerProject
{
    public struct Priority
    {
        public const int Lowest = 0;
        public const int Low = 1;
        public const int Normal = 2;
        public const int High = 3;
        public const int Highest = 4;
    }

    public struct Category
    {
        public const string General = "General";
        public const string Database = "Database";
        public const string GUI = "GUI";
        public const string Audit = "Audit";    // ### agregar logs para esto
    }

    public static class TheLogger
    {
        static TheLogger()
        {
            try
            {
                Logger.SetLogWriter(new LogWriterFactory().Create());
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception while initializing the Logger: " + ex.Message);
                throw;
            }
        }

        public static void Write(string message, TraceEventType severity = TraceEventType.Information
            , string category = Category.General, int priority = Priority.Normal)
        {
            LogEntry l = new LogEntry()
            {
                Categories = new string[] { category },
                Priority = priority,
                Severity = severity,
                Message = message
            };
            Logger.Write(l);
        }
    }
}
