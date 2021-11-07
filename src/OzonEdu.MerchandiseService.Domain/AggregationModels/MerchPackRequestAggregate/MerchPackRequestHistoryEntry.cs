using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.BaseTypes;
using OzonEdu.MerchandiseService.Domain.Exceptions;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate
{
    public class MerchPackRequestHistoryEntry : Entity
    {
        public MerchPackRequestId Id { get; }
        
        public Employee Employee { get; }
        
        public MerchPackName MerchPackName { get; }
        
        public List<Sku> SkuList { get; }
        
        public DateTime CompletedAt { get; private set; }
        
        public MerchPackRequestHistoryEntryStatus Status { get; private set; }

        public MerchPackRequestHistoryEntry(
            Employee employee,
            MerchPackName name,
            List<Sku> skuList,
            DateTime timeOfCompletion,
            MerchPackRequestId id)
        {
            Employee = employee;
            Status = MerchPackRequestHistoryEntryStatus.WaitingForEmployeeToTakeIt;
            MerchPackName = name;
            SkuList = skuList;
            CompletedAt = timeOfCompletion;
            Id = id;
        }

        public void SetRequestToCompleted()
        {
            if (!Status.Equals(MerchPackRequestHistoryEntryStatus.WaitingForEmployeeToTakeIt))
            {
                throw new MerchRequestHistoryEntryStatusException(
                    $"Attempting invalid transition from {Status.Name} to {MerchPackRequestHistoryEntryStatus.WaitingForEmployeeToTakeIt.Name}");
            }
            Status = MerchPackRequestHistoryEntryStatus.WaitingForEmployeeToTakeIt;
        }
    }
}