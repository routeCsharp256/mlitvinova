using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.HttpModels;

namespace OzonEdu.MerchandiseService.HttpClient
{
    public interface IMerchandiseHttpClient
    {
        Task<GetMerchPackDetailsResponse> V1GetMerchPackDetails(string name, CancellationToken token);
        Task<GetMerchPackIssuedToEmployeeResponse> V1GetMerchIssuedToEmployee(long employeeId, CancellationToken token);
    }
}