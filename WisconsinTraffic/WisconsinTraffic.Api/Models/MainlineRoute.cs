using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WITraffic511.Api.Models
{
    /// <summary>
    /// Returns the route configuration and status in the 511wi system. This includes travel time and average speed for interchange to interchange freeway segments updated every 60 seconds.
    /// </summary>
    [JsonObject]
    public class MainlineRoute
    {
        [JsonProperty]
        public string DirectionOfTravel { get; set; }

        [JsonProperty]
        public int EndLatitude { get; set; }

        [JsonProperty]
        public int EndLongitude { get; set; }
        
        [JsonProperty]
        public string Id { get; set; }

        [JsonProperty]
        public int LengthMeters { get; set; }

        [JsonProperty]
        public object Links { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public string RoadwayName { get; set; }

        [JsonProperty]
        public double? SpeedMetersPerHour { get; set; }

        public double? SpeedMilesPerHour
        {
            get
            {
                if (SpeedMetersPerHour != null)
                {
                    return SpeedMetersPerHour*2.2;
                }
                return null;
            }
        }

        [JsonProperty]
        public int StartLatitude { get; set; }

        [JsonProperty]
        public int StartLongitude { get; set; }

        [JsonProperty]
        public int? TravelTimeMinutes { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
