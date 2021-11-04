using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.BaseTypes;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate
{
    public class MerchPackRequestHistoryEntry : Entity
    {
        public Employee Employee { get; }
        
        public MerchPackName MerchPackName { get; }
        
        public List<Sku> SkuList { get; }
        
        public DateTime? CompletedAt { get; private set; }
        
        public MerchPackRequestStatus Status { get; private set; }

        public MerchPackRequestHistoryEntry(
            Employee employee,
            MerchPackName name,
            List<Sku> skuList)
        {
            Employee = employee;
            Status = MerchPackRequestStatus.Created;
            MerchPackName = name;
            SkuList = skuList;
        }

        public void SetRequestWaitingForSupplies()
        {
            Status = MerchPackRequestStatus.WaitingForSupplies;
        }

        public void CompleteRequest(DateTime timeOfCompletion)
        {
            Status = MerchPackRequestStatus.Completed;
            CompletedAt = timeOfCompletion;
        }

        public void SendRequestToStock()
        {
            Status = MerchPackRequestStatus.SentToStock;
        }
    }
}