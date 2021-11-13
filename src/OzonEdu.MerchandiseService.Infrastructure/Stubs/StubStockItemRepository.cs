using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Contracts;

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

        public IUnitOfWork UnitOfWork { get; }
        public Task<StockItem> CreateAsync(StockItem itemToCreate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<StockItem> UpdateAsync(StockItem itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}