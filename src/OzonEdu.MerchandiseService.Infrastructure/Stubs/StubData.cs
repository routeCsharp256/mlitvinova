using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestHistoryEntryAggregate;
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
                new MerchItem(ItemType.Notepad, new List<GenericMerchConstraint>())
            });

        public static readonly MerchPack WelcomePack = new MerchPack(
            new MerchPackName("Welcome pack"), new List<MerchItem>()
            {
                new MerchItem(ItemType.Pen, new List<GenericMerchConstraint>())
            });

        public static readonly MerchPack GoodbyePack = new MerchPack(
            new MerchPackName("Goodbye pack"), new List<MerchItem>()
            {
                new MerchItem(ItemType.Socks, new List<GenericMerchConstraint>())
            });
        
        public static readonly List<MerchPack> AvailablePacks = new List<MerchPack>
        {
            StarterPack,
            WelcomePack,
            GoodbyePack
        };

        public readonly List<MerchPackRequest> MerchIssuing = new()
        {
            new MerchPackRequest(
                new Employee(1),
                StarterPack,
                new List<IMerchConstraint>()
                {
                    new GenericMerchConstraint("Color", "red")
                }),
            new MerchPackRequest(
                new Employee(2),
                WelcomePack,
                new List<IMerchConstraint>()
                {
                    new GenericMerchConstraint("Color", "red")
                })
        };
        
        public readonly List<MerchPackRequestHistoryEntry> MerchIssued = new()
        {
            new MerchPackRequestHistoryEntry(
                new Employee(1),
                WelcomePack.Name,
                new List<Sku>
                {
                    new Sku(1)
                },
                DateTime.MinValue)
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