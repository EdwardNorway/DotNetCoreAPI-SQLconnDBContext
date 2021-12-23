using System.Text.Json.Serialization;

namespace DotNetCoreAPI.Models
{    public class Order
    {
        [JsonPropertyName("Id")]
        public long Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("IsInventory")]
        public bool IsInventory { get; set; }
        [JsonPropertyName("Description")]
        public string Description { get; set; }
    }
}