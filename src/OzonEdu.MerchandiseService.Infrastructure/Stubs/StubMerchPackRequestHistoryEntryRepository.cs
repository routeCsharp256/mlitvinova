using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestHistoryEntryAggregate;
using OzonEdu.MerchandiseService.Domain.Contracts;

namespace OzonEdu.MerchandiseService.Infrastructure.Stubs
{
    public class StubMerchPackRequestHistoryEntryRepository : IMerchPackRequestHistoryEntryRepository
    {
        public IUnitOfWork UnitOfWork { get; }
        public Task<MerchPackRequestHistoryEntry> CreateAsync(MerchPackRequestHistoryEntry itemToCreate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<MerchPackRequestHistoryEntry> UpdateAsync(MerchPackRequestHistoryEntry itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<MerchPackRequestHistoryEntry>> FindByEmployeeAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}