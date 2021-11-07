using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
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
    }
}