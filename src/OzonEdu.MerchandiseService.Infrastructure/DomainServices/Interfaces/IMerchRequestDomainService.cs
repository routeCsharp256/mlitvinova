using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainServices.Interfaces
{
    public interface IMerchRequestDomainService
    {
        Task GiveOutMerch(int employeeId, string merchPackName, Dictionary<string, string> constraints,
            CancellationToken token);

        Task<List<MerchIssuedToEmployee>> GetMerchIssuedToEmployee(int employeeId,
            CancellationToken token);

        Task GiveOutPreparedPack(int employeeId, string packName, CancellationToken token);

        Task ProcessNewSupplyArrival(List<long> skuArrived, CancellationToken token);

        Task<MerchPack> GetMerchPackContent(string packName, CancellationToken token);
    }
}