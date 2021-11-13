using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.BaseTypes;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.StockItemAggregate
{
    public class Item : Entity
    {
        public ItemType Type { get; }

        public Item(ItemType type)
        {
            Type = type;
        }
    }
}