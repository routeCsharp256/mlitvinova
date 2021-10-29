namespace OzonEdu.MerchandiseService.Domain.Models
{
    public class MerchPackInStatus
    {
        public MerchPackInStatus(string name, MerchPurchaseStatus status)
        {
            MerchPackName = name;
            Status = status;
        }
        
        public string MerchPackName { get; }
        
        public MerchPurchaseStatus Status { get; }
    }
}