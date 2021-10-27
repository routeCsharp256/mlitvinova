using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.Models;
using MerchandiseService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MerchandiseService.Services
{
    public class MerchandiseService : IMerchandiseService
    {
        #region TestData
       
        private MerchPack StarterPack => new MerchPack(
            "Starter pack", new List<MerchItem>()
            {
                new MerchItem("Блокнот", new Dictionary<string, string>()
                {
                    {"Цвет", "Красный"}
                })
            });
        
        private MerchPack WelcomePack => new MerchPack(
            "Welcome pack", new List<MerchItem>()
            {
                new MerchItem("Ручка", new Dictionary<string, string>()
                {
                    {"Цвет", "Синий"}
                })
            });
        
        private List<MerchPack> AvailablePacks => new List<MerchPack>
        {
            StarterPack,
            WelcomePack
        };
            
        private Dictionary<long, List<(string, MerchPurchaseStatus)>> MerchIssued => new()
        {
            {
                1, new List<(string, MerchPurchaseStatus)>()
                {
                    ("Starter pack", MerchPurchaseStatus.Issued)
                }
            }
        };
        
        #endregion

        public Task<bool> IssueMerchToEmployee(long employeeId, string merchPackName, CancellationToken token)
        {
            if (!AvailablePacks.Any(x => x.MerchPackName.Equals(merchPackName)))
            {
                return Task.FromResult(false);
            }
            
            var itemToAdd = (merchPackName, MerchPurchaseStatus.Issuing);
            if (MerchIssued.ContainsKey(employeeId))
            {
                MerchIssued[employeeId].Add(itemToAdd);
            }
            else
            {
                MerchIssued.Add(employeeId, new List<(string, MerchPurchaseStatus)>() { itemToAdd });
            }

            return Task.FromResult(true);
        }

        public Task<List<(string merchPackName, MerchPurchaseStatus status)>> GetIssuedMerchToEmployee(
            long employeeId,
            CancellationToken token)
        {
            if (MerchIssued.ContainsKey(employeeId))
            {
                return Task.FromResult(MerchIssued[employeeId]);
            }

            return Task.FromResult(new List<(string, MerchPurchaseStatus)>());
        }

        public Task<MerchPack?> GetMerchPackContent(string merchPackName, CancellationToken token)
        {
            var packDetails = AvailablePacks.FirstOrDefault(x => x.MerchPackName.Equals(merchPackName));
            return Task.FromResult(packDetails);
        }
    }
}