using OzonEdu.MerchandiseService.Domain.BaseTypes;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate
{
    public class Employee : Entity, IAggregationRoot
    {
        public long Id { get; }

        public Employee(long employeeId)
        {
            Id = employeeId;
        }
    }
}