using OzonEdu.MerchandiseService.Domain.BaseTypes;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate
{
    public class MerchConstraintType : Enumeration
    {
        public static MerchConstraintType ClothingSizeConstraintType = new MerchConstraintType(1, nameof(ClothingSizeConstraintType));
        public static MerchConstraintType ColorConstraintType = new MerchConstraintType(2, nameof(ColorConstraintType));
        
        public MerchConstraintType(int id, string name) : base(id, name)
        {
        }
    }
}