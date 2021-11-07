using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Exceptions;

namespace OzonEdu.MerchandiseService.Infrastructure.Extensions
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