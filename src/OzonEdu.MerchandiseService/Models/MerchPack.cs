using System.Collections.Generic;

namespace OzonEdu.MerchandiseService.Models
{
    public class MerchPack
    {
        public MerchPack(string merchPackName, List<MerchItem> packItems)
        {
            MerchPackName = merchPackName;
            PackItems = packItems;
        }

        public string MerchPackName { get; }
        
        public List<MerchItem> PackItems { get; }
    }
}