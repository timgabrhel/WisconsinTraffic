using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WITraffic511.Api.Models
{
    [JsonObject]
    public class MainlineLink
    {
        [JsonProperty]
        public string DirectionOfTravel { get; set; }

        [JsonProperty]
        public float EndLatitude { get; set; }

        [JsonProperty]
        public float EndLongitude { get; set; }

        [JsonProperty]
        public string Id { get; set; }

        [JsonProperty]
        public string RoadwayName { get; set; }

        [JsonProperty]
        public int? SpeedMetersPerHour { get; set; }

        public double? SpeedMilesPerHour
        {
            get
            {
                if (SpeedMetersPerHour != null)
                {
                    return SpeedMetersPerHour * 2.2;
                }
                return null;
            }
        }

        [JsonProperty]
        public float StartLatitude { get; set; }

        [JsonProperty]
        public float StartLongitude { get; set; }

        public override string ToString()
        {
            return RoadwayName + " " + DirectionOfTravel;
        }
    }
}
