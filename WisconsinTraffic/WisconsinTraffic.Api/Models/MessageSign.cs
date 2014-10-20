using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WITraffic511.Api.Models
{
    [JsonObject]
    public class MessageSign
    {
        [JsonProperty]
        public string DirectionOfTravel { get; set; }

        [JsonProperty]
        public string Id { get; set; }

        [JsonProperty]
        public float Latitude { get; set; }

        [JsonProperty]
        public float Longitude { get; set; }

        [JsonProperty]
        public string[] Messages { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public string Roadway { get; set; }
    }
}
