using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WITraffic511.Api.Tests
{
    public class LoggingService : ILoggingService
    {
        public void Info(string message, string category)
        {
            Trace.WriteLine(message, category);
        }

        public void Warn(string message, string category)
        {
            Trace.WriteLine(message, category);
        }

        public void Error(string message, string category)
        {
            Trace.WriteLine(message, category);
        }

        public void Error(Exception exception, string category)
        {
            Trace.WriteLine(exception.ToString(), category);
        }

        public void Error(string message, Exception exception, string category)
        {
            Trace.WriteLine(message + exception, category);
        }
    }
}
