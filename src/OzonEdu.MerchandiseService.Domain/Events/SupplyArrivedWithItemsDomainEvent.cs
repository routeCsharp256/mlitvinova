using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;

namespace OzonEdu.MerchandiseService.Domain.Events
{
    public class SupplyArrivedWithItemsDomainEvent : INotification
    {
        public Sku StockItemSku { get; }
        public int Quantity { get; }

        public SupplyArrivedWithItemsDomainEvent(
            Sku stockItemSku,
            int quantity)
        {
            StockItemSku = stockItemSku;
            Quantity = quantity;
        }
    }
}