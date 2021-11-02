using OzonEdu.MerchandiseService.Domain.BaseTypes;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects
{
    public class MerchItemType : Enumeration
    {
        public MerchItemType(int id, string name) : base(id, name)
        {
        }
    }
}