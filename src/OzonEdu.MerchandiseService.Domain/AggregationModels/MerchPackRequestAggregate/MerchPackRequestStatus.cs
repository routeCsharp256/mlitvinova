using OzonEdu.MerchandiseService.Domain.BaseTypes;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate
{
    public class MerchPackRequestStatus : Enumeration
    {
        public static MerchPackRequestStatus Issuing = new(1, nameof(Issuing));
        
        public static MerchPackRequestStatus Issued = new(2, nameof(Issued));
        
        public MerchPackRequestStatus(int id, string name) : base(id, name)
        {
        }
    }
}