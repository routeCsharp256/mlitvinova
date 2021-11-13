using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestHistoryEntryAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Exceptions;
using Xunit;

namespace OzonEdu.MerchandiseService.Domain.Tests.Domain
{
    public class MerchPackRequestHistoryEntryTests
    {
        [Fact]
        public void ChangePackRequestStatus_ChangeFromValidStatus_ShouldExecuteCorrectly()
        {
            var employee = new Employee(1);
            var merchPackName = new MerchPackName("Test pack");
            var skuList = new List<Sku>();

            var request = new MerchPackRequestHistoryEntry(employee, merchPackName, skuList, DateTime.Now);
            
            request.SetRequestToCompleted();
            
            Assert.Equal(MerchPackRequestHistoryEntryStatus.Completed, request.Status);
        }
        
        [Fact]
        public void ChangePackRequestStatus_ChangeFromInvalidStatus_ShouldThrowException()
        {
            var employee = new Employee(1);
            var merchPackName = new MerchPackName("Test pack");
            var skuList = new List<Sku>();

            var request = new MerchPackRequestHistoryEntry(employee, merchPackName, skuList, DateTime.Now);
            request.SetRequestToCompleted();

            Assert.Throws<MerchRequestHistoryEntryStatusException>(() => request.SetRequestToCompleted());
        }
    }
}