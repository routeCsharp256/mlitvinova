using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.HttpModels;

namespace OzonEdu.MerchandiseService.HttpClient
{
    public interface IMerchandiseHttpClient
    {
        Task<GetMerchPackDetailsResponse> GetMerchPackContent(string name, CancellationToken token);
        
        Task<IssueMerchToEmployeeResponse> IssueMerchToEmployee(long employeeId, string merchName,
            CancellationToken token);
        
        Task<GetMerchPackIssuedToEmployeeResponse> GetMerchIssuedToEmployee(long employeeId, CancellationToken token);

        Task<GiveOutPreparedPackResponse> GiveOutPreparedPack(int employeeId, string packName, CancellationToken token);

        Task<ProcessNewSupplyArrivalResponse> ProcessNewSupplyArrival(List<long> skuArrived, CancellationToken token);

    }
}