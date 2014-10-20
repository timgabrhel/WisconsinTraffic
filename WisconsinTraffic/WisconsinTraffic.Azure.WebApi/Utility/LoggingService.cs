using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using WITraffic511.Api;

namespace WisconsinTraffic.Azure.WebApi.Utility
{
    public class LoggingService : ILoggingService
    {
        private ApiServices _services;

        public LoggingService(ApiServices services)
        {
            _services = services;
        }

        public void Info(string message, string category)
        {
            _services.Log.Info(message, category: category);
        }

        public void Warn(string message, string category)
        {
            _services.Log.Warn(message, category: category);
        }

        public void Error(string message, string category)
        {
            _services.Log.Error(message, category: category);
        }

        public void Error(Exception exception, string category)
        {
            _services.Log.Error(exception, category: category);
        }

        public void Error(string message, Exception exception, string category)
        {
            _services.Log.Error(message, exception, category: category);
        }
    }
}