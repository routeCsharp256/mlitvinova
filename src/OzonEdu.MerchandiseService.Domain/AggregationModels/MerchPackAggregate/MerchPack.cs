using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.BaseTypes;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate
{
    public class MerchPack : Entity, IAggregationRoot
    {
        public List<MerchItem> MerchPackItems { get; }
        
        public MerchPackName Name { get; }
        
        public MerchPack(MerchPackName name, List<MerchItem> items)
        {
            Name = name;
            MerchPackItems = items;
        }
    }
}