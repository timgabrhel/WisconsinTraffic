using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using WITraffic511.Api.Models;

namespace WisconsinTraffic.Azure.WebApi.Models
{
    [JsonObject]
    public class TrafficResult<T>
    {
        [JsonProperty]
        public DateTime Timestamp { get; set; }

        [JsonProperty]
        public List<T> Items { get; set; }

        public TrafficResult()
        {
            Timestamp = DateTime.UtcNow.AddHours(-5);
            Items = new List<T>();
        }

        internal TrafficResult(TrafficDocument<T> doc)
        {
            Items = doc.Items;
            Timestamp = doc.Timestamp;
        }
    }
}