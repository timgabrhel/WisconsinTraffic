using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WITraffic511.Api.Models
{
    [JsonObject]
    public class Roadwork
    {
        [JsonProperty]
        public string CountyName { get; set; }

        [JsonProperty]
        public string Description { get; set; }

        [JsonProperty]
        public string DirectionOfTravel { get; set; }

        [JsonProperty]
        public string Id { get; set; }

        [JsonProperty]
        public string LanesAffected { get; set; }

        [JsonProperty]
        public string LastUpdated { get; set; }

        [JsonProperty]
        public float Latitude { get; set; }

        [JsonProperty]
        public object[] LcsEntries { get; set; }

        [JsonProperty]
        public string Location { get; set; }

        [JsonProperty]
        public float Longitude { get; set; }

        [JsonProperty]
        public string PlannedEndDate { get; set; }

        [JsonProperty]
        public string Reported { get; set; }

        [JsonProperty]
        public string RoadwayName { get; set; }

        [JsonProperty]
        public string Severity { get; set; }

        [JsonProperty]
        public string StartDate { get; set; }
    }
}
