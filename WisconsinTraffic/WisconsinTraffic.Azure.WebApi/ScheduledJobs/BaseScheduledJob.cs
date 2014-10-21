using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Controllers;
using PortableRest;
using WisconsinTraffic.Azure.WebApi.DocumentDb;
using WisconsinTraffic.Azure.WebApi.Models;
using WisconsinTraffic.Azure.WebApi.Utility;
using WITraffic511.Api;

namespace WisconsinTraffic.Azure.WebApi.ScheduledJobs
{
    public abstract class BaseScheduledJob<T> : ScheduledJob
    {
        public DocumentEntityDomainManager<TrafficDocument<T>> DomainManager; 
        public ITrafficClient ApiClient;
        private string _wiTraffic511ApiKey;
        private bool _wiTraffic511UseMockData;
        private ILoggingService _loggingService;
    
        public override Task ExecuteAsync()
        {
            _loggingService = new LoggingService(Services);
            
            string WITrafficDocumentDbName;
            if (!(Services.Settings.TryGetValue("WITrafficDocumentDbName", out WITrafficDocumentDbName)))
            {
                Services.Log.Error("Could not retrieve WITrafficDocumentDbName", category: "JobExecution");
                return Task.FromResult(false);
            }

            string WITrafficDocumentDbCollectionName;
            if (!(Services.Settings.TryGetValue("WITrafficDocumentDbCollectionName", out WITrafficDocumentDbCollectionName)))
            {
                Services.Log.Error("Could not retrieve WITrafficDocumentDbCollectionName", category: "JobExecution");
                return Task.FromResult(false);
            }

            DomainManager = new DocumentEntityDomainManager<TrafficDocument<T>>(WITrafficDocumentDbName, WITrafficDocumentDbCollectionName, Services);

            if (!(Services.Settings.TryGetValue("WITraffic511ApiKey", out _wiTraffic511ApiKey)))
            {
                Services.Log.Error("Could not retrieve WITraffic511ApiKey", category: "JobExecution");
                return Task.FromResult(false);
            }

            var tmpUseMock = "";
            if (!(Services.Settings.TryGetValue("WITraffic511UseMockData", out tmpUseMock)))
            {
                Services.Log.Warn("Could not retrieve WITraffic511UseMockData", category: "JobExecution");
            }
            else
            {
                if (!bool.TryParse(tmpUseMock, out _wiTraffic511UseMockData))
                {
                    Services.Log.Warn("UseMock511WIData is not a valid boolean", category: "JobExecution");
                }
            }

            if (_wiTraffic511UseMockData)
            {
                ApiClient = new MockTrafficClient(_loggingService);
            }
            else
            {
                ApiClient = new TrafficClient(_wiTraffic511ApiKey);
            }
            
            Execute();

            return Task.FromResult(true);
        }

        public virtual Task Execute()
        {
            return Task.FromResult(true);
        }

        protected async Task ProcessResponse(RestResponse<List<T>> response, string identifier)
        {
            if (response.HttpResponseMessage.IsSuccessStatusCode)
            {
                var doc = new TrafficDocument<T>()
                {
                    Id = identifier,
                    Items = response.Content,
                    Timestamp = DateTime.UtcNow.AddHours(-5)
                };

                if (!DomainManager.Exists(identifier))
                {
                    await DomainManager.InsertAsync(doc);
                }
                else
                {
                    await DomainManager.ReplaceAsync(identifier, doc);
                }
                Services.Log.Info(identifier + "Job Complete.", category: "JobExecution");
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