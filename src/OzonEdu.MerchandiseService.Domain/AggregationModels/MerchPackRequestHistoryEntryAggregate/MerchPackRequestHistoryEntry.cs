using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.BaseTypes;
using OzonEdu.MerchandiseService.Domain.Events;
using OzonEdu.MerchandiseService.Domain.Exceptions;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestHistoryEntryAggregate
{
    public class MerchPackRequestHistoryEntry : Entity
    {
        public Employee Employee { get; }
        
        public MerchPackName MerchPackName { get; }
        
        public List<Sku> SkuList { get; }
        
        public DateTime CompletedAt { get; }
        
        public MerchPackRequestHistoryEntryStatus Status { get; private set; }

        public MerchPackRequestHistoryEntry(
            Employee employee,
            MerchPackName name,
            List<Sku> skuList,
            DateTime timeOfCompletion)
        {
            Employee = employee;
            Status = MerchPackRequestHistoryEntryStatus.WaitingForEmployeeToTakeIt;
            MerchPackName = name;
            SkuList = skuList;
            CompletedAt = timeOfCompletion;
        }

        public void SetRequestToCompleted()
        {
            if (!Status.Equals(MerchPackRequestHistoryEntryStatus.WaitingForEmployeeToTakeIt))
            {
                throw new MerchRequestHistoryEntryStatusException(
                    $"Attempting invalid transition from {Status.Name} to {MerchPackRequestHistoryEntryStatus.WaitingForEmployeeToTakeIt.Name}");
            }
            Status = MerchPackRequestHistoryEntryStatus.WaitingForEmployeeToTakeIt;
            
            AddMerchPackRequestCompletedEvent();
        }

        private void AddMerchPackRequestCompletedEvent()
        {
            this.AddDomainEvent(new MerchRequestHasBeenCompletedDomainEvent(this));
        }
    }
}