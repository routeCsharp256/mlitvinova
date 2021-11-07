using System;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.BaseTypes;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.StockItemAggregate
{
    public class StockItem : Entity, IAggregationRoot
    {
        public StockItem(Sku sku,
            Name name,
            Item itemType,
            ClothingSize size,
            Quantity quantity)
        {
            Sku = sku;
            Name = name;
            ItemType = itemType;
            SetQuantity(quantity);
            SetClothingSize(size);
        }

        public Sku Sku { get; }
        
        public Name Name { get; }
        
        public Item ItemType { get; }
        
        public ClothingSize ClothingSize { get; private set; }
        
        public Quantity Quantity { get; private set; }

        public Tag Tag { get; set; }

        private void SetClothingSize(ClothingSize size)
        {
            if (size is not null && ItemType.Type.IsClothes)
            {
                ClothingSize = size;
            }
            else 
            {
                ClothingSize = null;
            }
        }
        
        private void SetQuantity(Quantity value)
        {
            Quantity = value;
        }
    }
}