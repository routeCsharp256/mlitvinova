using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestHistoryEntryAggregate;
using OzonEdu.MerchandiseService.Domain.Contracts;

namespace OzonEdu.MerchandiseService.Infrastructure.Repositories.Implementation.Stubs
{
    public class StubMerchPackRequestHistoryEntryRepository : IMerchPackRequestHistoryEntryRepository
    {
        // TODO: Actual Unit of Work implementation.
        public IUnitOfWork UnitOfWork { get; } = new StubUnitOfWork();
        
        private readonly StubData _stubData;
        
        public StubMerchPackRequestHistoryEntryRepository(StubData data)
        {
            _stubData = data;
        }
        
        public Task<MerchPackRequestHistoryEntry> CreateAsync(MerchPackRequestHistoryEntry itemToCreate, CancellationToken cancellationToken = default)
        {
            _stubData.MerchIssued.Add(itemToCreate);
            return Task.FromResult(itemToCreate);
        }

        public Task<MerchPackRequestHistoryEntry> UpdateAsync(MerchPackRequestHistoryEntry itemToUpdate, CancellationToken cancellationToken = default)
        {
            var toUpdate = _stubData.MerchIssued.Select(x => x.Id == itemToUpdate.Id);
            return Task.FromResult(itemToUpdate);
        }

        public Task<List<MerchPackRequestHistoryEntry>> FindByEmployeeAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_stubData.MerchIssued.FindAll(x => x.Employee.Equals(employee)));
        }
    }
}