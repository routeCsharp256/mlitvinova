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

        public Task<List<MerchPackRequest>> FindAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_stubData.MerchIssuing);
        }

        public Task<List<MerchPackRequest>> FindByEmployeeAsync(Employee employee,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_stubData.MerchIssuing.FindAll(x => x.EmployeeId.Equals(employee)));
        }

        public Task DeleteAsync(MerchPackRequest request, CancellationToken cancellationToken = default)
        {
            _stubData.MerchIssuing.Remove(request);
            return Task.CompletedTask;
        }
    }
}