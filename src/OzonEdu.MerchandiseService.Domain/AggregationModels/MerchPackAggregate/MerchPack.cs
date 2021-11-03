using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.BaseTypes;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate
{
    public class MerchPack : Entity
    {
        public MerchPack(List<MerchItem> items)
        {
            MerchPackItems = items;
        }
        
        public List<MerchItem> MerchPackItems { get; }
    }
}