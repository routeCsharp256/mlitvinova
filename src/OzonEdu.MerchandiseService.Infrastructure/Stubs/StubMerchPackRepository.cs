using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.Stubs
{
    public class StubMerchPackRepository : IMerchPackRepository
    {
        public Task<MerchPack> GetMerchPackByName(MerchPackName name, CancellationToken token = default)
        {
            return Task.FromResult(StubData.AvailablePacks.FirstOrDefault(x => x.Name.Equals(name)));
        }

        public Task<List<MerchPack>> GetAllMerchPacks(CancellationToken token = default)
        {
            return Task.FromResult(StubData.AvailablePacks);
        }

        public Task<bool> MerchPackExists(MerchPackName name, CancellationToken token = default)
        {
            var packExists = StubData.AvailablePacks.Any(x => x.Name.Equals(name));
            return Task.FromResult(packExists);
        }
    }
}