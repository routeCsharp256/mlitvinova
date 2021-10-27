using System.Collections.Generic;

namespace OzonEdu.MerchandiseService.Models
{
    public class MerchItem
    {
        public MerchItem(string name, Dictionary<string, string> properties = null)
        {
            this.Name = name;
            this.Properties = properties ?? new Dictionary<string, string>();
        }
        
        public string Name { get; }
        
        public Dictionary<string, string> Properties { get; }
    }
}