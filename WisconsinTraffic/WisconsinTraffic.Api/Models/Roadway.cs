using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WITraffic511.Api.Models
{
    [JsonObject]
    public class Roadway
    {
        [JsonProperty]
        public string RoadwayName { get; set; }

        [JsonProperty]
        public int SortOrder { get; set; }
    }
}
