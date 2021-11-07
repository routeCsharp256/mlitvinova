using OzonEdu.MerchandiseService.Domain.BaseTypes;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate
{
    public class MerchPackRequestStatus : Enumeration
    {
        public static MerchPackRequestStatus Completed = new(1, nameof(Completed));

        public static MerchPackRequestStatus WaitingForEmployeeToTakeIt = new(2, nameof(WaitingForEmployeeToTakeIt));
        
        public static MerchPackRequestStatus WaitingForSupplies = new(3, nameof(WaitingForSupplies));
        
        public MerchPackRequestStatus(int id, string name) : base(id, name)
        {
        }
    }
}