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
        
        public string Name { get; }
        public bool IsClothes { get; }
    }
}