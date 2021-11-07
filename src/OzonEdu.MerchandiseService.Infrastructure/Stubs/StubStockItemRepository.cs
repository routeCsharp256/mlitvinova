using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;

namespace OzonEdu.MerchandiseService.Infrastructure.Stubs
{
    public class StubStockItemRepository : IStockItemRepository
    {
        public Task<List<StockItem>> GetAllStockItems(CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> GiveOutSkus(List<Sku> skus, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}