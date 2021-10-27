using System.Collections.Generic;

namespace OzonEdu.MerchandiseService.Models
{
    public class MerchItem
    {
        public MerchItem(string name, Dictionary<string, string> properties = null)
        {
            this.Name = name;
            this.Properties = properties;
        }
        
        public string Name { get; }
        
        public Dictionary<string, string> Properties { get; }
    }
}