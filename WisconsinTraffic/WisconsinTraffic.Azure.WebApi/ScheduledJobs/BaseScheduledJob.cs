using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using PortableRest;
using WisconsinTraffic.Azure.WebApi.Models;
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

        protected async Task ProcessResponse<T>(RestResponse<List<T>> response, string identifier) where T : class
        {
            if (response.HttpResponseMessage.IsSuccessStatusCode)
            {
                var doc = new TrafficDocument<T>()
                {
                    Id = identifier,
                    Items = response.Content,
                    Timestamp = DateTime.UtcNow.AddHours(-5)
                };

                using (var db = new TrafficDocumentProvider())
                {
                    await db.Init();
                    await db.Save(doc);
                }
                Services.Log.Info(identifier + "Job Complete.", category: identifier + "Job.Execute()");
            }
            else
            {
                var sb = new StringBuilder();
                response.Exception.WriteExceptionDetails(sb, 0);
                Services.Log.Error(sb.ToString());
            }
        }
    }
}