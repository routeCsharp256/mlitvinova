using System.Text.Json.Serialization;

namespace OzonEdu.MerchandiseService.HttpModels
{
    public class IssueMerchToEmployeeResponse
    {
        
        [JsonPropertyName("IssueMerchResponse")]
        public IssueMerchResponse IssueMerchResponse { get; set; } 
    }

    public enum IssueMerchResponse
    {
        Created,
        NoSuchMerch,
        MerchAlreadyIssued,
        Unknown
    }
}