using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.Models;

namespace MerchandiseService.Services.Interfaces
{
    public interface IMerchandiseService
    {
        Task<bool> IssueMerchToEmployee(long employeeId, string merchPackName, CancellationToken token);

        Task<List<(string merchPackName, MerchPurchaseStatus status)>> GetIssuedMerchToEmployee(long employeeId, CancellationToken token);

        Task<MerchPack?> GetMerchPackContent(string merchPackName, CancellationToken token);
    }
}