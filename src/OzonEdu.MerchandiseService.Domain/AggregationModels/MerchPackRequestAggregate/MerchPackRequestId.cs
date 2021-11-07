using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.BaseTypes;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate
{
    public class MerchPackRequestId : ValueObject
    {
        public long Id { get; }

        public MerchPackRequestId(long id)
        {
            Id = id;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}