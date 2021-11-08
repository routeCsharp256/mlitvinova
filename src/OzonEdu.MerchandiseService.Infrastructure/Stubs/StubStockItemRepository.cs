using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;

namespace OzonEdu.MerchandiseService.Infrastructure.Stubs
{
    public class StubStockItemRepository : IStockItemRepository
    {
        private readonly StubData _stubData;

        public StubStockItemRepository(StubData data)
        {
            _stubData = data;
        }
        
        public Task<List<StockItem>> GetAllStockItems(CancellationToken token)
        {
            return Task.FromResult(_stubData.AllItems);
        }

        public Task<bool> GiveOutSkus(List<Sku> skus, CancellationToken token)
        {
            return Task.FromResult(true);
        }
    }
}