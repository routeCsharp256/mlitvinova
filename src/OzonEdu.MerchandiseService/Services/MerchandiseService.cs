using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.Models;
using MerchandiseService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MerchandiseService.Services
{
    public class MerchandiseService : IMerchandiseService
    {
        private static MerchPack StarterPack = new MerchPack("Starter pack", new List<MerchItem>()
        {
            new MerchItem("Блокнот", new Dictionary<string, string>()
            {
                {"Цвет", "Красный"}
            })
        });

        private readonly Dictionary<long, List<(MerchPack, MerchPurchaseStatus)>> MerchIssued = new()
        {
            {
                1, new List<(MerchPack, MerchPurchaseStatus)>()
                {
                    (StarterPack, MerchPurchaseStatus.Issued)
                }
            }
        };

        public Task IssueMerchToEmployee(long employeeId, MerchPack pack, CancellationToken token)
        {
            var itemToAdd = (pack, MerchPurchaseStatus.Issuing);
            if (MerchIssued.ContainsKey(employeeId))
            {
                MerchIssued[employeeId].Add(itemToAdd);
            }
            else
            {
                MerchIssued.Add(employeeId, new List<(MerchPack, MerchPurchaseStatus)>() { itemToAdd });
            }

            return Task.CompletedTask;
        }

        public Task<List<(MerchPack, MerchPurchaseStatus)>> GetIssuedMerchToEmployee(long employeeId,
            CancellationToken token)
        {
            if (MerchIssued.ContainsKey(employeeId))
            {
                return Task.FromResult(MerchIssued[employeeId]);
            }

            return Task.FromResult(new List<(MerchPack, MerchPurchaseStatus)>());
        }
    }
}