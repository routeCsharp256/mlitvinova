using System.Collections.Generic;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.Queries.MerchRequestAggregate
{
    public class GetMerchRequestsByEmployeeQueue: IRequest<List<(MerchPackName, MerchPackRequestStatus)>>
    {
        public long EmployeeId { get; set; }
    }
}