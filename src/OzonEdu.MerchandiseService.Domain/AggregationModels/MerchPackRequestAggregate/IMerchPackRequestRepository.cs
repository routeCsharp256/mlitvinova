using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.Contracts;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate
{
    public interface IMerchPackRequestRepository : IRepository<MerchPackRequestHistoryEntry>
    {
        Task<List<MerchPackRequestHistoryEntry>> FindByEmployeeAsync(Employee employee, CancellationToken cancellationToken = default);

        Task<List<MerchPackRequestHistoryEntry>> FindByIdAsync(long id, CancellationToken cancellationToken = default);

        Task<List<MerchPackRequest>> GetAllUnfulfilledRequests(CancellationToken cancellationToken = default);

        Task AddSuccessfullyOrderedRequest(MerchPackRequestHistoryEntry historyEntry, CancellationToken cancellationToken = default);

        Task AddWaitingForCompletionRequest(MerchPackRequest request, CancellationToken token = default);
    }
}