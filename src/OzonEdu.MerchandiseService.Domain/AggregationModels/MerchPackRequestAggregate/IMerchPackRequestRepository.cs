using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.Contracts;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate
{
    public interface IMerchPackRequestRepository : IRepository<MerchPackRequest>
    {
        Task<List<MerchPackRequest>> FindAllAsync(CancellationToken cancellationToken = default);

        Task<List<MerchPackRequest>> FindByEmployeeAsync(Employee employee, CancellationToken cancellationToken = default);

        Task DeleteAsync(MerchPackRequest request, CancellationToken cancellationToken = default);
    }
}