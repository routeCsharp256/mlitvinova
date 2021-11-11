using OzonEdu.MerchandiseService.Domain.AggregationModels.StockItemAggregate;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate
{
    public interface IMerchConstraint
    {
        bool SatisfiesConstraint(StockItem sku);

        string Key();
        string Value();
    }
}