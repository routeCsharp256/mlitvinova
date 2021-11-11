using System.Text.Json.Serialization;

namespace OzonEdu.MerchandiseService.HttpModels
{
    public class GiveOutPreparedPackResponse
    {
        [JsonPropertyName("status")]
        public GiveOutPreparedPackStatus Status { get; set; }
    }
    
    // Reserved for future error handling.
    public enum GiveOutPreparedPackStatus
    {
        Ok
    }
}