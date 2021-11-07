using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.StockItemAggregate
{
    public interface IStockItemRepository
    {
        Task<List<StockItem>> GetAllStockItems(CancellationToken token);

        Task<bool> GiveOutSkus(List<Sku> skus, CancellationToken token);
    }
}