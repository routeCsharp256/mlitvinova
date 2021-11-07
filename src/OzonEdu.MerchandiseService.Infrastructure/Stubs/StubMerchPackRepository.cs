using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.Stubs
{
    public class StubMerchPackRepository : IMerchPackRepository
    {
        public Task<MerchPack> GetMerchPackByName(MerchPackName name, CancellationToken token = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<MerchPack>> GetAllMerchPacks(CancellationToken token = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> MerchPackExists(MerchPackName name, CancellationToken token = default)
        {
            throw new System.NotImplementedException();
        }
    }
}