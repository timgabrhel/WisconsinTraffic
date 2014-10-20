using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WITraffic511.Api
{
    public interface ILoggingService
    {
        void Info(string message, string category);

        void Warn(string message, string category);

        void Error(string message, string category);

        void Error(Exception exception, string category);

        void Error(string message, Exception exception, string category);
    }
}
