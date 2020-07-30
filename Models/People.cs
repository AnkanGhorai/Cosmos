using Newtonsoft.Json;

namespace Cosmos.Models
{
    public class People
    {
            [JsonProperty(PropertyName = "id")]
            public string Id { get; set; }

            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "surName")]
            public string Surname { get; set; }

            [JsonProperty(PropertyName = "age")]
            public int Age { get; set; }
        
    }
}
