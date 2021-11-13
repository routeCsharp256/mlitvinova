using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.Domain.BaseTypes;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate
{
    public class MerchIssuedToEmployee : Entity
    {
        public MerchPackName Name { get; }
        public MerchPackRequestStatus Status { get; }

        public MerchIssuedToEmployee(MerchPackName name, MerchPackRequestStatus status)
        {
            Name = name;
            Status = status;
        }
    }
}