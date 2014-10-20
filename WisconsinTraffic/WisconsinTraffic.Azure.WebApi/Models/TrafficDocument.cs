using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WisconsinTraffic.Azure.WebApi.Models
{
    [JsonObject]
    public class TrafficDocument<T>
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty]
        public DateTime Timestamp { get; set; }

        [JsonProperty]
        public List<T> Items { get; set; } 
    }
}