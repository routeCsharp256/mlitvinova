using System.Threading;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate
{
    public interface IEmployeeRepository
    {
        public Task<List<MerchIssuedToEmployee>> GetMerchIssuedToEmployee(Employee employee,
            CancellationToken token = default);

        public Task<bool> IsMerchAlreadyReceived(Employee employee, MerchPackName name, CancellationToken token = default);
    }
}