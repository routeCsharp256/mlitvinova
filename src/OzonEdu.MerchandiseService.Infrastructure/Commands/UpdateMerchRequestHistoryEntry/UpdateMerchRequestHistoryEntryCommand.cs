using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestHistoryEntryAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.Commands.UpdateMerchRequestHistoryEntry
{
    public class UpdateMerchRequestHistoryEntryCommand : IRequest
    {
        public MerchPackRequestHistoryEntry Request { get; }
    }
}