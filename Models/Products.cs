using Newtonsoft.Json;

namespace Cosmos.Models
{
    public class Products
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName="type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName ="Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName ="Model")]
        public string Model { get; set; }

    }
}
