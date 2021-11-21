using Newtonsoft.Json;

namespace BloggingWebAPI.Models
{
    public class EmptyDocument<T>
    {
        [JsonProperty("id")]
        public string Id { get; set; } = "";
        [JsonProperty("document")]
        public T? Document { get; set; }
        public string PartitionKey { get; set; } = "";
    }
}
