using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.BaseTypes;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects
{
    public class Sku : ValueObject
    {
        public long Value { get; }
        
        public Sku(long sku)
        {
            Value = sku;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}