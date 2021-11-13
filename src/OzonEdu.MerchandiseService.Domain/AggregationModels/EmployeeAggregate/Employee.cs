using OzonEdu.MerchandiseService.Domain.BaseTypes;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate
{
    public sealed class Employee : Entity
    {
        public Employee(int employeeId)
        {
            Id = employeeId;
        }
    }
}