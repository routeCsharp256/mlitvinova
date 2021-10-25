using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.Models;

namespace MerchandiseService.Services.Interfaces
{
    public interface IMerchandiseService
    {
        Task IssueMerchToEmployee(long employeeId, MerchPack pack, CancellationToken token);

        Task<List<(MerchPack, MerchPurchaseStatus)>> GetIssuedMerchToEmployee(long employeeId, CancellationToken token);
    }
}