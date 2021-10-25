using System.Collections.Generic;

namespace MerchandiseService.Models
{
    public class MerchPack
    {
        public MerchPack(List<MerchItem> packItems)
        {
            this.PackItems = packItems;
        }
        
        public List<MerchItem> PackItems { get; }
    }
}