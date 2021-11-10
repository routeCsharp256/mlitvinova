using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.Contracts;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestHistoryEntryAggregate
{
    public interface IMerchPackRequestHistoryEntryRepository : IRepository<MerchPackRequestHistoryEntry>
    {
        Task<List<MerchPackRequestHistoryEntry>> FindByEmployeeAsync(
            Employee employee, CancellationToken cancellationToken = default);
    }
}