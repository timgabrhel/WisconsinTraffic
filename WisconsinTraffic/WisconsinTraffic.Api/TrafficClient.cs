using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using PortableRest;
using WITraffic511.Api.Models;

namespace WITraffic511.Api
{
    public class TrafficClient : RestClient, ITrafficClient
    {
        private string ApiKey;

        public TrafficClient(string apiKey)
        {
            ApiKey = apiKey;
            BaseUrl = "http://www.511wi.gov/web/api/";
        }

        public async Task<RestResponse<List<MainlineRoute>>> GetMainlineRoutesAsync()
        {
            var request = GetPopulatedRequest("mainlineroutes");
            return await SendAsync<List<MainlineRoute>>(request);
        }

        public async Task<RestResponse<List<Incident>>> GetIncidentsAsync()
        {
            var request = GetPopulatedRequest("incidents");
            return await SendAsync<List<Incident>>(request);
        }

        public async Task<RestResponse<List<MainlineLink>>> GetMainlineLinksAsync()
        {
            var request = GetPopulatedRequest("mainlinelinks");
            return await SendAsync<List<MainlineLink>>(request);
        }

        public async Task<RestResponse<List<WinterRoadCondition>>> GetWinterRoadConditionsAsync()
        {
            var request = GetPopulatedRequest("winterroadconditions");
            return await SendAsync<List<WinterRoadCondition>>(request);
        }

        public async Task<RestResponse<List<Camera>>> GetCamerasAsync()
        {
            var request = GetPopulatedRequest("cameras");
            return await SendAsync<List<Camera>>(request);
        }

        public async Task<RestResponse<List<Roadway>>> GetRoadwaysAsync()
        {
            var request = GetPopulatedRequest("roadways");
            return await SendAsync<List<Roadway>>(request);
        }

        public async Task<RestResponse<List<Roadwork>>> GetRoadworkAsync()
        {
            var request = GetPopulatedRequest("roadwork");
            return await SendAsync<List<Roadwork>>(request);
        }

        public async Task<RestResponse<List<MessageSign>>> GetMessageSignsAsync()
        {
            var request = GetPopulatedRequest("messagesigns");
            return await SendAsync<List<MessageSign>>(request);
        }

        public async Task<RestResponse<List<Alert>>> GetAlertsAsync()
        {
            var request = GetPopulatedRequest("alerts");
            return await SendAsync<List<Alert>>(request);
        }

        /// <summary>
        /// Gets a new <see cref="RestRequest"/> populated with the common values for every request.
        /// </summary>
        /// <param name="resourceUrl"></param>
        /// <returns>A new <see cref="RestRequest"/> populated with the common values for every request</returns>
        private RestRequest GetPopulatedRequest(string resourceUrl)
        {
            if (string.IsNullOrWhiteSpace(ApiKey))
            {
                throw new ArgumentNullException(ApiKey, "A required Api Key was missing.");
            }

            var request = new RestRequest(resourceUrl) { ContentType = ContentTypes.Json };
            request.AddQueryString("key", ApiKey);
            request.AddQueryString("format", "json");

            return request;
        }
    }
}
