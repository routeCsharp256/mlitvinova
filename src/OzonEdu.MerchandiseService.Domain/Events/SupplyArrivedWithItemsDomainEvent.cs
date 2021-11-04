using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;

namespace OzonEdu.MerchandiseService.Domain.Events
{
    public class SupplyArrivedWithItemsDomainEvent : INotification
    {
        public Sku StockItemSku { get; }

        public SupplyArrivedWithItemsDomainEvent(Sku stockItemSku)
        {
            StockItemSku = stockItemSku;
        }
    }
}