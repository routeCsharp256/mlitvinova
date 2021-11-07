using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate
{
    public interface IMerchPackRepository
    {
        Task<MerchPack> GetMerchPackByName(MerchPackName name,
            CancellationToken token = default);

        Task<List<MerchPack>> GetAllMerchPacks(CancellationToken token = default);

        Task<bool> MerchPackExists(MerchPackName name, CancellationToken token = default);
    }
}