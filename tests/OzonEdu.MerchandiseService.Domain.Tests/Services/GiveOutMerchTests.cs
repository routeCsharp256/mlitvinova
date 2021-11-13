using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Exceptions;
using Xunit;

namespace OzonEdu.MerchandiseService.Domain.Tests.Services
{
    public class GiveOutMerchTests : ServicesTestBase
    {
        [Fact]
        public async Task GiveOutMerch_CorrectMerchRequest_ShouldExecuteSuccessfully()
        {
            var employeeId = 3;
            var merchPackName = "Starter pack";
            
            await _service.GiveOutMerch(
                employeeId, merchPackName, new() { }, CancellationToken.None);

            var repositoryContents = await _merchPackRequestHistoryEntryRepo.FindByEmployeeAsync(
                new Employee(employeeId),
                CancellationToken.None);

            Assert.Contains(repositoryContents, x => x.MerchPackName.Value.Equals(merchPackName));
        }

        [Fact]
        public async Task GiveOutMerch_MerchAlreadyIssued_ShouldThrow()
        {
            await Assert.ThrowsAsync<MerchAlreadyIssuedException>(() =>
                _service.GiveOutMerch(1, "Starter pack", new() { }, CancellationToken.None));
        }
    }
}