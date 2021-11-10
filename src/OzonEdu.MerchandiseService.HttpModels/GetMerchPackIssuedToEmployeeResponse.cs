using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OzonEdu.MerchandiseService.HttpModels
{
    public class GetMerchPackIssuedToEmployeeResponse
    {
        [JsonPropertyName("merchList")]
        public List<MerchPackInStatus> MerchList { get; set; }
    }

    public class MerchPackInStatus
    {
        [JsonPropertyName("merchPackName")]
        public string MerchPackName { get; set; }

        [JsonPropertyName("status")]
        public MerchPackStatus Status { get; set; }
    }

    public enum MerchPackStatus
    {
        Issued,
        Issuing
    }
}