using System.Text.Json.Serialization;

namespace OzonEdu.MerchandiseService.HttpModels
{
    public class ProcessNewSupplyArrivalResponse
    {
        [JsonPropertyName("status")]
        public ProcessNewSupplyArrivalStatus Status { get; set; }
    }
    
    // Reserved for future error handling.
    public enum ProcessNewSupplyArrivalStatus
    {
        Ok
    }
}