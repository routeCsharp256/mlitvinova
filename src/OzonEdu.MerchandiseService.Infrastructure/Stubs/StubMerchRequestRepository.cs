using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestHistoryEntryAggregate;
using OzonEdu.MerchandiseService.Domain.Contracts;

namespace OzonEdu.MerchandiseService.Infrastructure.Stubs
{
    public class StubMerchRequestRepository : IMerchPackRequestRepository
    {
        private readonly StubData _stubData;

        public StubMerchRequestRepository(StubData data)
        {
            _stubData = data;
        }

        public IUnitOfWork UnitOfWork { get; }

        public Task<MerchPackRequest> CreateAsync(MerchPackRequest itemToCreate,
            CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<MerchPackRequest> UpdateAsync(MerchPackRequest itemToUpdate,
            CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<MerchPackRequestHistoryEntry> CreateAsync(
            MerchPackRequestHistoryEntry itemToCreate,
            CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<MerchPackRequestHistoryEntry> UpdateAsync(MerchPackRequestHistoryEntry itemToUpdate,
            CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<MerchPackRequest>> FindAllAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<MerchPackRequest>> FindByEmployeeAsync(Employee employee,
            CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<MerchPackRequestHistoryEntry>> FindByIdAsync(long id,
            CancellationToken cancellationToken = default)
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

        public Task<List<MerchIssuedToEmployee>> GetMerchIssuedToEmployee(
            Employee employee,
            CancellationToken token = default)
        {
            var result = _stubData.MerchIssued[employee.Id]
                .Select(x => new MerchIssuedToEmployee(x.Item1, x.Item2))
                .ToList();
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