using Newtonsoft.Json;

namespace WITraffic511.Api.Models
{
    [JsonObject]
    public class Alert
    {
        [JsonProperty]
        public string[] CountyNames { get; set; }

        [JsonProperty]
        public string Id { get; set; }

        [JsonProperty]
        public string Message { get; set; }

        [JsonProperty]
        public object Notes { get; set; }
    }
}
