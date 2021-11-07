using OzonEdu.MerchandiseService.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate
{
    public class ClothingSizeMerchConstraint : IMerchConstraint
    {
        public ClothingSize Size { get; }
        
        public ClothingSizeMerchConstraint(ClothingSize size)
        {
            Size = size;
        }

        public bool SatisfiesConstraint(StockItem stockItem)
        {
            bool isAppliable = stockItem.ItemType.Type.IsClothes;
            return !isAppliable || stockItem.ClothingSize.Equals(Size);
        }
    }
}