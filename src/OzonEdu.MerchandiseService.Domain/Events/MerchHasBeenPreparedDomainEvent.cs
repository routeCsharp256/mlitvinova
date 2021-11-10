using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;

namespace OzonEdu.MerchandiseService.Domain.Events
{
    public class MerchHasBeenPreparedDomainEvent : INotification
    {
        public Employee Employee { get; }
        public MerchPackName MerchPackName { get; }

        public MerchHasBeenPreparedDomainEvent(Employee employee, MerchPackName name)
        {
            Employee = employee;
            MerchPackName = name;
        }
    }
}