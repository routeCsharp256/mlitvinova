using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate
{
    public class MerchPackRequestHistoryEntry
    {
        public Employee Employee { get; }
        
        public string MerchPackName { get; }
        
        public List<Sku> SkuList { get; }
    }
}