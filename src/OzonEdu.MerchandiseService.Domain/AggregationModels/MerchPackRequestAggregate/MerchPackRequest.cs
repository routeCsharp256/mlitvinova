using System.Collections.Generic;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate
{
    public class MerchPackRequest
    {
        public long EmployeeId { get; }
        public string MerchPack { get; }
        public Dictionary<string, string> MerchPackConstraints { get; }
    }
}