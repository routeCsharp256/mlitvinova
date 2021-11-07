using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.Stubs
{
    public class StubEmployeeRepository : IEmployeeRepository
    {
        private readonly StubData _stubData;

        public StubEmployeeRepository(StubData data)
        {
            _stubData = data;
        }
        
        public Task<List<(MerchPackName, MerchPackRequestStatus)>> GetMerchIssuedToEmployee(
            Employee employee, 
            CancellationToken token = default)
        {
            var result = _stubData.MerchIssued[employee.Id];
            return Task.FromResult(result);
        }

        public Task<bool> IsMerchAlreadyReceived(
            Employee employee, 
            MerchPackName name, 
            CancellationToken token = default)
        {
            var merchIssued = _stubData.MerchIssued.ContainsKey(employee.Id)
                              && _stubData.MerchIssued[employee.Id].Any(x => x.Item1.Equals(name));

            return Task.FromResult(merchIssued);
        }
    }
}