using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OzonEdu.MerchandiseService.Domain.Tests.Services
{
    public class ProcessNewArrivalTests : ServicesTestBase
    {
        [Fact]
        public async Task ProcessNewArrival_ShouldExecuteCorrectly()
        {
            await _service.ProcessNewSupplyArrival(new List<long>() {1, 2}, CancellationToken.None);
            
            var unfulfilledRequests = await _merchPackRequestRepo.FindAllAsync(CancellationToken.None);
            Assert.Empty(unfulfilledRequests);
        }
    }
}