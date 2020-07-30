using Newtonsoft.Json;

namespace Cosmos.Models
{
    public class Places
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "zip")]
        public string Zip { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }
        
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }
    }
}
