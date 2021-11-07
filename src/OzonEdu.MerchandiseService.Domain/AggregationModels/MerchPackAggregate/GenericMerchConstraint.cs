using OzonEdu.MerchandiseService.Domain.AggregationModels.StockItemAggregate;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate
{
    public class GenericMerchConstraint : IMerchConstraint
    {
        public GenericMerchConstraint(string propertyName, string propertyValue)
        {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
        }
        
        public string PropertyName { get; }
        public string PropertyValue { get; }
        
        public bool SatisfiesConstraint(StockItem stockItem)
        {
            var genericProperties = stockItem.Tag;
            // possibly somehow validate stock item tag I don't really understand that 
            return true;
        }
    }
}