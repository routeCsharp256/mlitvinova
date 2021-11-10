using System.Collections.Generic;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.Queries.MerchRequestAggregate
{
    public class GetMerchRequestsByEmployeeQueue: IRequest<List<MerchIssuedToEmployee>>
    {
        public int EmployeeId { get; set; }
    }
}