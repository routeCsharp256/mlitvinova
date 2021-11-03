using OzonEdu.MerchandiseService.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate
{
    public class ClothingSizeMerchConstraint : IMerchConstraint
    {
        public ClothingSizeMerchConstraint(ClothingSize size)
        {
            Size = size;
        }
        
        public ClothingSize Size { get; }
        
        public bool SatisfiesConstraint(StockItem sku)
        {
            if (!sku.ItemType.Type.IsClothes)
            {
                return false;
            }
            else
            {
                return sku.ClothingSize.Equals(Size);
            }
        }
    }
}