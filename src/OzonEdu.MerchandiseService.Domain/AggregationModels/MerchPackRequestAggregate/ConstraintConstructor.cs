using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate
{
    public static class ConstraintConstructor
    {
        public static IMerchConstraint ConstructConstraint(string name, string value)
        {
            if (name.Equals(MerchConstraintType.ClothingSizeConstraintType.Name))
            {
                return new ClothingSizeMerchConstraint(ClothingSize.Parse(value));
            }

            return new GenericMerchConstraint(name, value);
        }
    }
}