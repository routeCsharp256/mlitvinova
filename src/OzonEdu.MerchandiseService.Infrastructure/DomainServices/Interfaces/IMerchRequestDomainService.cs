using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainServices.Interfaces
{
    public interface IMerchRequestDomainService
    {
        Task GiveOutMerch(int employeeId, string merchPackName, Dictionary<string, string> constraints,
            CancellationToken token);

        Task<List<MerchIssuedToEmployee>> GetMerchIssuedToEmployee(int employeeId,
            CancellationToken token);
    }
}