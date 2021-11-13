using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OzonEdu.MerchandiseService.Domain.Tests.Services
{
    public class GetMerchIssuedToEmployeeTests : ServicesTestBase
    {
        [Fact]
        public async Task GetMerchIssuedToEmployee_EmployeeExists_ShouldReturnNonEmptyResult()
        {
            var employeeId = 1;
            
            var result = await _service.GetMerchIssuedToEmployee(
                employeeId, CancellationToken.None);
            
            Assert.Equal(2, result.Count);
        }
    }
}