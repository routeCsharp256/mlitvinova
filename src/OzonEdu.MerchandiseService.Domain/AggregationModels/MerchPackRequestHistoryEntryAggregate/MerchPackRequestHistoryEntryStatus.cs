using System;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.Domain.BaseTypes;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestHistoryEntryAggregate
{
    public class MerchPackRequestHistoryEntryStatus : Enumeration
    {
        public static MerchPackRequestHistoryEntryStatus Completed = new(1, nameof(Completed));

        public static MerchPackRequestHistoryEntryStatus WaitingForEmployeeToTakeIt = new(2, nameof(WaitingForEmployeeToTakeIt));

        // todo: rewrite
        public MerchPackRequestStatus ToMerchPackRequestStatus()
        {
            if (this.Equals(Completed))
            {
                return MerchPackRequestStatus.Completed;
            }

            if (this.Equals(WaitingForEmployeeToTakeIt))
            {
                return MerchPackRequestStatus.WaitingForEmployeeToTakeIt;
            }

            throw new Exception("Unable to parse status");
        }
        
        public MerchPackRequestHistoryEntryStatus(int id, string name) : base(id, name)
        {
        }
    }
}