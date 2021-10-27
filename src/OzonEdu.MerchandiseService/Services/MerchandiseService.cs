using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Models;
using OzonEdu.MerchandiseService.Services.Interfaces;

namespace OzonEdu.MerchandiseService.Services
{
    public class MerchandiseService : IMerchandiseService
    {
        #region TestData

        private static readonly MerchPack StarterPack = new MerchPack(
            "Starter pack", new List<MerchItem>()
            {
                new MerchItem("Блокнот", new Dictionary<string, string>()
                {
                    {"Цвет", "Красный"}
                })
            });

        private static readonly MerchPack WelcomePack = new MerchPack(
            "Welcome pack", new List<MerchItem>()
            {
                new MerchItem("Ручка", new Dictionary<string, string>()
                {
                    {"Цвет", "Синий"}
                })
            });

        private static readonly List<MerchPack> AvailablePacks = new List<MerchPack>
        {
            StarterPack,
            WelcomePack
        };

        private readonly Dictionary<long, List<MerchPackInStatus>> _merchIssued = new()
        {
            {
                1, new List<MerchPackInStatus>()
                {
                    new MerchPackInStatus("Starter pack", MerchPurchaseStatus.Issued)
                }
            }
        };

        #endregion

        public Task<MerchIssueRequestStatus> IssueMerchToEmployee(long employeeId, string merchPackName,
            CancellationToken token)
        {
            if (!AvailablePacks.Any(x => x.MerchPackName.Equals(merchPackName)))
            {
                return Task.FromResult(MerchIssueRequestStatus.NoSuchMerchExists);
            }

            var itemToAdd = new MerchPackInStatus(merchPackName, MerchPurchaseStatus.Issuing);
            if (_merchIssued.ContainsKey(employeeId))
            {
                var merchIssuedToEmployee = _merchIssued[employeeId];
                if (merchIssuedToEmployee.Any(x => x.MerchPackName.Equals(merchPackName)))
                {
                    return Task.FromResult(MerchIssueRequestStatus.EmployeeAlreadyHasSuchMerch);
                }

                merchIssuedToEmployee.Add(itemToAdd);
                return Task.FromResult(MerchIssueRequestStatus.RequestCreated);
            }

            _merchIssued.Add(employeeId, new List<MerchPackInStatus>() {itemToAdd});
            return Task.FromResult(MerchIssueRequestStatus.RequestCreated);
        }

        public Task<List<MerchPackInStatus>> GetIssuedMerchToEmployee(
            long employeeId,
            CancellationToken token)
        {
            if (_merchIssued.ContainsKey(employeeId))
            {
                var result = _merchIssued[employeeId];
                return Task.FromResult(result);
            }

            return Task.FromResult(new List<MerchPackInStatus>());
        }

        public Task<MerchPack?> GetMerchPackContent(string merchPackName, CancellationToken token)
        {
            var packDetails = AvailablePacks.FirstOrDefault(x => x.MerchPackName.Equals(merchPackName));
            return Task.FromResult(packDetails);
        }
    }
}