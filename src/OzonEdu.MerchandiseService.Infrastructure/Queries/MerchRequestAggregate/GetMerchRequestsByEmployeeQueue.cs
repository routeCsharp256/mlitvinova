using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.Queries.MerchRequestAggregate
{
    public class GetMerchRequestsByEmployeeQueue: IRequest<MerchPackRequestHistoryEntry>
    {
        public long EmployeeId { get; set; }
    }
}