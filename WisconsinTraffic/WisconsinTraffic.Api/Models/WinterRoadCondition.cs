using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WITraffic511.Api.Models
{
    [JsonObject]
    public class WinterRoadCondition
    {
        [JsonProperty]
        public string Condition { get; set; }

        [JsonProperty]
        public string EndCounty { get; set; }

        [JsonProperty]
        public string LocationDescription { get; set; }

        [JsonProperty]
        public string Region { get; set; }

        [JsonProperty]
        public string StartCounty { get; set; }

        public override string ToString()
        {
            return StartCounty + " - " + EndCounty;
        }
    }
}
