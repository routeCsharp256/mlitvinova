using System.Linq;
using OzonEdu.MerchandiseService.Domain.BaseTypes;
using OzonEdu.MerchandiseService.Domain.Exceptions;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate
{
    public class ClothingSize : Enumeration
    {
        public static ClothingSize XS = new(1, nameof(XS));
        public static ClothingSize S = new(2, nameof(S));
        public static ClothingSize M = new(3, nameof(M));
        public static ClothingSize L = new(4, nameof(L));
        public static ClothingSize XL = new(5, nameof(XL));
        public static ClothingSize XXL = new(6, nameof(XXL));

        public ClothingSize(int id, string name) : base(id, name)
        {
        }
        
        public static ClothingSize Parse(string value)
        {
            if (!IsClothingSize(value))
            {
                throw new InvalidConstraintType($"Wrong clothing size {value}");
            }

            return GetAll<ClothingSize>().First(x => x.Name.Equals(value));
        }
        
        private static bool IsClothingSize(string value)
        {
            var allAvailableSizes = GetAll<ClothingSize>();
            return allAvailableSizes.Any(x => x.Name.Equals(value));
        }

    }
}