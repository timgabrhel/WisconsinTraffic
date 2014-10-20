using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using WisconsinTraffic.Azure.WebApi.Utility;
using WITraffic511.Api;

namespace WisconsinTraffic.Azure.WebApi.ScheduledJobs
{
    public abstract class BaseScheduledJob : ScheduledJob
    {
        public ITrafficClient ApiClient;
        private string _wiTraffic511Key;
        private bool _useMock511WIData;
        private ILoggingService _loggingService;
    
        public override Task ExecuteAsync()
        {
            _loggingService = new LoggingService(Services);

            if (!(Services.Settings.TryGetValue("511wi-apikey", out _wiTraffic511Key)))
            {
                Services.Log.Error("Could not retrieve 511wi-apikey", category: "BaseScheduledJob.ExecuteAsync()");
                return Task.FromResult(false);
            }

            var tmpUseMock = "";
            if (!(Services.Settings.TryGetValue("UseMock511WIData", out tmpUseMock)))
            {
                Services.Log.Warn("Could not retrieve UseMock511WIData", category: "BaseScheduledJob.ExecuteAsync()");
            }
            else
            {
                if (!bool.TryParse(tmpUseMock, out _useMock511WIData))
                {
                    Services.Log.Warn("UseMock511WIData is not a valid boolean", category: "BaseScheduledJob.ExecuteAsync()");
                }
            }

            if (_useMock511WIData)
            {
                ApiClient = new MockTrafficClient(_loggingService);
            }
            else
            {
                ApiClient = new TrafficClient(_wiTraffic511Key);
            }
            
            Execute();

            return Task.FromResult(true);
        }

        public virtual Task Execute()
        {
            return Task.FromResult(true);
        }
    }
}