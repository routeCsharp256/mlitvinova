using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestHistoryEntryAggregate;

namespace OzonEdu.MerchandiseService.Domain.Events
{
    public class MerchRequestHasBeenCompletedDomainEvent: INotification
    {
        public MerchPackRequestHistoryEntry Entry { get; }

        public MerchRequestHasBeenCompletedDomainEvent(MerchPackRequestHistoryEntry entry)
        {
            Entry = entry;
        }
    }
}