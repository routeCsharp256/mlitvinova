using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Models;
using MerchItem = OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate.MerchItem;
using MerchPack = OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate.MerchPack;

namespace OzonEdu.MerchandiseService.Infrastructure.Stubs
{
    public class StubData
    {
        public static readonly MerchPack StarterPack = new MerchPack(
            new MerchPackName("Starter pack"), 
            new List<MerchItem>()
            {
                new MerchItem(ItemType.Notepad, new List<GenericMerchConstraint>()
                {
                    new GenericMerchConstraint("Color", "Red")
                })
            });

        public static readonly MerchPack WelcomePack = new MerchPack(
            new MerchPackName("Welcome pack"), new List<MerchItem>()
            {
                new MerchItem(ItemType.Pen, new List<GenericMerchConstraint>()
                {
                    new GenericMerchConstraint("Цвет", "Синий")
                })
            });

        public static readonly List<MerchPack> AvailablePacks = new List<MerchPack>
        {
            StarterPack,
            WelcomePack
        };
        
        public readonly Dictionary<long, List<(MerchPackName, MerchPackRequestStatus)>> MerchIssued = new()
        {
            {
                1, new List<(MerchPackName, MerchPackRequestStatus)>()
                {
                    (StarterPack.Name, MerchPackRequestStatus.WaitingForSupplies)
                }
            }
        };

        public readonly List<StockItem> AllItems = new()
        {
            new StockItem(
                new Sku(1),
                new Name("Pen"),
                new Item(ItemType.Pen),
                null,
                new Quantity(15))
            {
                Tag = new Tag("Color:red")
            },
            new StockItem(
                new Sku(2),
                new Name("Notepad"),
                new Item(ItemType.Notepad),
                null,
                new Quantity(15))
            {
                Tag = new Tag("Color:red,print:hello")
            },
        };
    }
}