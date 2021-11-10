using OzonEdu.MerchandiseService.Domain.BaseTypes;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate
{
    public class Employee : Entity
    {
        public long Id { get; }
        
        public Employee(long employeeId)
        {
            Id = employeeId;
        }
    }
}