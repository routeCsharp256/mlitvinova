using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate
{
    public class MerchItem
    {
        public MerchItem(ItemType type,
            List<GenericMerchConstraint> constraints)
        {
            Constraints = constraints;
            ItemType = type;
        }
        
        public ItemType ItemType { get; }
        
        public List<GenericMerchConstraint> Constraints { get; }
    }
}