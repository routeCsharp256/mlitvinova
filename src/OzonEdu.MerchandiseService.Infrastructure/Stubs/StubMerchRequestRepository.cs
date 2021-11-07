using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.Domain.Contracts;

namespace OzonEdu.MerchandiseService.Infrastructure.Stubs
{
    public class StubMerchRequestRepository : IMerchPackRequestRepository
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

        public Task<List<MerchPackRequestHistoryEntry>> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<MerchPackRequest>> GetAllUnfulfilledRequests(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task AddSuccessfullyOrderedRequest(MerchPackRequestHistoryEntry historyEntry,
            CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task AddWaitingForCompletionRequest(MerchPackRequest request, CancellationToken token = default)
        {
            throw new System.NotImplementedException();
        }
    }
}