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

        public List<Sku> FilterByConstraints(List<StockItem> allItems)
        {
            CheckAbilityToFormPackIfThereIsClothes();

            var skuList = new List<Sku>();
            foreach (var packItem in MerchPack.MerchPackItems)
            {
                var sku = GetSkuByConstraint(packItem, allItems);
                skuList.Add(sku);
            }

            return skuList;
        }

        private Sku GetSkuByConstraint(MerchItem item, List<StockItem> allItems)
        {
            var itemsByType = allItems.Where(x => x.ItemType.Type.Equals(item.ItemType)).ToList();
            if (!itemsByType.Any())
            {
                throw new UnableToFormMerchRequestException($"No packs of type {item.ItemType}");
            }

            var validItems = itemsByType
                .Where(x => ItemSatisfiesConstraint(x, PackConstraints, item.Constraints))
                .ToList();

            if (!validItems.Any())
            {
                throw new UnableToFormMerchRequestException($"Unable to form pack, conflicting constraints {item.ItemType}");
            }

            return validItems.FirstOrDefault(x => x.Quantity.Value > 0)?.Sku ??
                   validItems.First().Sku;
        }

        private bool ItemSatisfiesConstraint(
            StockItem item, 
            List<IMerchConstraint> packConstraints,
            List<GenericMerchConstraint> itemConstraints)
        {
            foreach (var itemConstraint in itemConstraints)
            {
                if (!itemConstraint.SatisfiesConstraint(item))
                {
                    return false;
                }
            }

            foreach (var merchPackConstraint in packConstraints)
            {
                if (!merchPackConstraint.SatisfiesConstraint(item))
                {
                    return false;
                }
            }

            return true;
        }
        
        private void CheckAbilityToFormPackIfThereIsClothes()
        {
            if (MerchPack.MerchPackItems.Any(x => x.ItemType.IsClothes))
            {
                if (PackConstraints.All(x => (ClothingSizeMerchConstraint) x == null))
                {
                    throw new UnableToFormMerchRequestException(
                        $"Need to have clothing size constraint for merch pack {MerchPack.Name.Value}");
                }
            }
        }
    }
}