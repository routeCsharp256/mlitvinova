using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using Xunit;

namespace OzonEdu.MerchandiseService.Domain.Tests.Domain
{
    public class MerchPackRequestTests
    {
        [Fact]
        public void ValidateConstraints_ConstraintsAreResolvable_NoClothes_ShouldExecuteSuccessfully()
        {
            var stockItems = new List<StockItem>()
            {
                new(new Sku(1), new Name("Test bag"), new Item(ItemType.Bag), null, new Quantity(10))
            };
            
            var merchPack = new MerchPack(new MerchPackName("Test"), new List<MerchItem>()
            {
                new(ItemType.Bag, new List<GenericMerchConstraint>()
                {
                    new GenericMerchConstraint("Print", "Test")
                })
            });
            var employee = new Employee(1);
            var constraints = new List<IMerchConstraint>()
            {
                new GenericMerchConstraint("Color", "Red")
            };

            var merchRequest = new MerchPackRequest(
                employee,
                merchPack,
                constraints);

            var filteredSku = merchRequest.FilterByConstraints(stockItems);
            Assert.Single(filteredSku);
        }
    }
}