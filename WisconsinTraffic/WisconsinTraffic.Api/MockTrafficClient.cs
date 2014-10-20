using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PortableRest;
using WITraffic511.Api.Models;

namespace WITraffic511.Api
{
    public class MockTrafficClient : ITrafficClient
    {
        private ILoggingService _loggingService;

        public MockTrafficClient(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public Task<RestResponse<List<MainlineRoute>>> GetMainlineRoutesAsync()
        {
            return Task.FromResult(DeserializeMockData<List<MainlineRoute>>("WITraffic511.Api.Mock.MainlineRoutes.json"));
        }

        public Task<RestResponse<List<Incident>>> GetIncidentsAsync()
        {
            return Task.FromResult(DeserializeMockData<List<Incident>>("WITraffic511.Api.Mock.Incidents.json"));
        }

        public Task<RestResponse<List<MainlineLink>>> GetMainlineLinksAsync()
        {
            return Task.FromResult(DeserializeMockData<List<MainlineLink>>("WITraffic511.Api.Mock.MainlineLinks.json"));
        }

        public Task<RestResponse<List<WinterRoadCondition>>> GetWinterRoadConditionsAsync()
        {
            return Task.FromResult(DeserializeMockData<List<WinterRoadCondition>>("WITraffic511.Api.Mock.WinterRoadConditions.json"));
        }

        public Task<RestResponse<List<Camera>>> GetCamerasAsync()
        {
            return Task.FromResult(DeserializeMockData<List<Camera>>("WITraffic511.Api.Mock.Cameras.json"));
        }

        public Task<RestResponse<List<Roadway>>> GetRoadwaysAsync()
        {
            return Task.FromResult(DeserializeMockData<List<Roadway>>("WITraffic511.Api.Mock.Roadways.json"));
        }

        public Task<RestResponse<List<Roadwork>>> GetRoadworkAsync()
        {
            return Task.FromResult(DeserializeMockData<List<Roadwork>>("WITraffic511.Api.Mock.Roadwork.json"));
        }

        public Task<RestResponse<List<MessageSign>>> GetMessageSignsAsync()
        {
            return Task.FromResult(DeserializeMockData<List<MessageSign>>("WITraffic511.Api.Mock.MessageSigns.json"));
        }

        public Task<RestResponse<List<Alert>>> GetAlertsAsync()
        {
            return Task.FromResult(DeserializeMockData<List<Alert>>("WITraffic511.Api.Mock.Alerts.json"));
        }

        private string GetMockFileData(string path)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var stream = assembly.GetManifestResourceStream(path);
                if (stream != null)
                {
                    using (var sr = new StreamReader(stream))
                    {
                        return sr.ReadToEnd();
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                _loggingService.Error("An error occured reading the mock data. " + path, ex, "MockTrafficClient.GetMockFileData()");
                return string.Empty;
            }
        }

        private RestResponse<T> DeserializeMockData<T>(string path) where T : class
        {
            RestResponse<T> result;
            try
            {
                var data = GetMockFileData(path);
                if (string.IsNullOrWhiteSpace(data) == false)
                {
                    var content = JsonConvert.DeserializeObject<T>(data);
                    result = new RestResponse<T>(new HttpResponseMessage(HttpStatusCode.OK), content);
                }
                else
                {
                    _loggingService.Warn("There was no content to deserialize.", "GetMockFileData.DeserializeMockData<T>()");
                    result = new RestResponse<T>(new HttpResponseMessage(HttpStatusCode.OK), default(T));
                }
            }
            catch (Exception ex)
            {
                _loggingService.Error("An error occured deserializing the mock data. " + path, ex, "GetMockFileData.DeserializeMockData<T>()");
                result = new RestResponse<T>(new HttpResponseMessage(HttpStatusCode.OK), default(T), ex);
            }
            return result;
        }
    }
}
