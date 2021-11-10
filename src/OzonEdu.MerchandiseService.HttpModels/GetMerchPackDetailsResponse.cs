using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OzonEdu.MerchandiseService.HttpModels
{
    public class GetMerchPackDetailsResponse
    {
        [JsonPropertyName("packName")]
        public string PackName { get; set; }
        
        [JsonPropertyName("items")]
        public List<MerchPackItem> Items { get; set; }
    }
    
    public class MerchPackItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set;  }
        
        [JsonPropertyName("properties")]
        public Dictionary<string, string> Properties { get; set; }
    }
}