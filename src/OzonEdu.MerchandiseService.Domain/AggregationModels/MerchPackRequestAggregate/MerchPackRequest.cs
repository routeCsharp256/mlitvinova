using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate
{
    public class MerchPackRequest
    {
        public long EmployeeId { get; }
        public string MerchPackName { get; }
        public List<IMerchConstraint> PackConstraints { get; }
    }
}