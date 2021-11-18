using System;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects
{
    public class ItemType
    {
        public static ItemType TShirt = new(nameof(TShirt), true);
        public static ItemType Sweatshirt = new(nameof(Sweatshirt), true);
        public static ItemType Notepad = new(nameof(Notepad), false);
        public static ItemType Bag = new( nameof(Bag), false);
        public static ItemType Pen = new(nameof(Pen), false);
        public static ItemType Socks = new(nameof(Socks), false);

        public ItemType(string name, bool isClothes)
        {
            Name = name;
            IsClothes = isClothes;
        }

        public static ItemType ToItemType(string name)
        {
            switch (name)
            {
                case "TShirt":
                    return TShirt;
                case "Sweatshirt":
                    return Sweatshirt;
                case "Bag":
                    return Bag;
                case "Socks":
                    return Socks;
                case "Pen":
                    return Pen;
                case "Notepad":
                    return Notepad;
                default:
                    throw new Exception($"Unknown merch type {name}");
            }
        }
        
        public string Name { get; }
        public bool IsClothes { get; }
    }
}