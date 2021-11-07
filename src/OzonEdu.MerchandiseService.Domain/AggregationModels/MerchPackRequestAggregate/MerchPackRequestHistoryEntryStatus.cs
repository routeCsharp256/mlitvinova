using System.Reflection.Metadata.Ecma335;
using OzonEdu.MerchandiseService.Domain.BaseTypes;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate
{
    public class MerchPackRequestHistoryEntryStatus : Enumeration
    {
        public static MerchPackRequestHistoryEntryStatus Completed = new(1, nameof(Completed));

        public static MerchPackRequestHistoryEntryStatus WaitingForEmployeeToTakeIt = new(2, nameof(WaitingForEmployeeToTakeIt));
        
        public MerchPackRequestHistoryEntryStatus(int id, string name) : base(id, name)
        {
        }
    }
}