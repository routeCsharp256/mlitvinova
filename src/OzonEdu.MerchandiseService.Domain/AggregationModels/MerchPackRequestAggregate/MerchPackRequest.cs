using System.Collections.Generic;
using System.Linq;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.BaseTypes;
using OzonEdu.MerchandiseService.Domain.Exceptions;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate
{
    public class MerchPackRequest : IAggregationRoot
    {
        public Employee EmployeeId { get; }
        public MerchPack MerchPack { get; }
        public List<IMerchConstraint> PackConstraints { get; }

        public MerchPackRequest(Employee employeeId,
            MerchPack merchPack,
            List<IMerchConstraint> constraints)
        {
            EmployeeId = employeeId;
            MerchPack = merchPack;
            PackConstraints = constraints;
        }

        // todo: move
        public List<Sku> FilterByConstraints(List<StockItem> allItems)
        {
            var packItems = MerchPack.MerchPackItems;

            var skuList = new List<Sku>();
            
            foreach (var packItem in packItems)
            {
                var packItemConstraints = packItem.Constraints;

                var itemsByType = allItems.Where(x => x.ItemType.Type.Equals(packItem.ItemType));

                var item = itemsByType.FirstOrDefault();
                
                // todo: filter by constraints
                if (item is null)
                {
                    throw new UnableToFormMerchRequestException($"Unable to find sku for {packItem.ItemType}");
                }
                
                skuList.Add(item.Sku);
            }

            return skuList;
        }
    }
}