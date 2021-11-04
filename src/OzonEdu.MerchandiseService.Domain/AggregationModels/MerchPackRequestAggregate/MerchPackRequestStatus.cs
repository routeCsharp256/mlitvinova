using System.Reflection.Metadata.Ecma335;
using OzonEdu.MerchandiseService.Domain.BaseTypes;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate
{
    public class MerchPackRequestStatus : Enumeration
    {
        public static MerchPackRequestStatus Created = new(1, nameof(Created));
        
        public static MerchPackRequestStatus SentToStock = new(1, nameof(SentToStock));
        
        public static MerchPackRequestStatus Completed = new(2, nameof(Completed));

        public static MerchPackRequestStatus WaitingForEmployeeToTakeIt = new(3, nameof(WaitingForEmployeeToTakeIt));

        public static MerchPackRequestStatus WaitingForSupplies = new(4, nameof(WaitingForSupplies));
        
        public MerchPackRequestStatus(int id, string name) : base(id, name)
        {
        }
    }
}