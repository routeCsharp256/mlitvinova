using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OzonEdu.MerchandiseService;
using OzonEdu.MerchandiseService.HttpClient;
using OzonEdu.MerchandiseService.HttpModels;
using OzonEdu.MerchandiseService.Infrastructure.Exceptions;

namespace TestHttpClient
{
    public class Tests
    {
        private TestServer _server;
        private MerchandiseHttpClient _client;
        
        [SetUp]
        public void Setup()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureServices(services =>
                {
                    services.AddControllers();
                }));
            _client = new MerchandiseHttpClient(_server.CreateClient());
        }

        [Test]
        public async Task TestGetMerchPackContents()
        {
            var nonexistentPack = await _client.GetMerchPackContent("123123", CancellationToken.None);
            
            Assert.IsNull(nonexistentPack.PackName);
            Assert.IsNull(nonexistentPack.Items);
            
            var existingPack = await _client.GetMerchPackContent("Starter pack", CancellationToken.None);
            
            Assert.AreEqual("Starter pack", existingPack.PackName);
            Assert.IsNotNull(existingPack.Items);
        }
        
        [Test]
        public async Task TestGetMerchIssuedToEmployee()
        {
            var employeeWithoutMerch = await _client.GetMerchIssuedToEmployee(3, CancellationToken.None);
            
            Assert.AreEqual(0, employeeWithoutMerch.MerchList.Count);
            
            var employeeWithMerch = await _client.GetMerchIssuedToEmployee(1, CancellationToken.None);
            
            Assert.AreEqual(2, employeeWithMerch.MerchList.Count);
            Assert.AreEqual(1, employeeWithMerch.MerchList.Count(
                s => s.Status.Equals(MerchPackStatus.WaitingForSupplies)));
            Assert.AreEqual(1, employeeWithMerch.MerchList.Count(
                s => s.Status.Equals(MerchPackStatus.WaitingForEmployeeToTakeIt)));
        }
        
        [Test]
        public async Task TestIssueMerchToEmployee()
        {
            var merchAlreadyIssued = await _client.IssueMerchToEmployee(1, "Starter pack", CancellationToken.None);
            Assert.AreEqual(IssueMerchResponse.MerchAlreadyIssued, merchAlreadyIssued.IssueMerchResponse);
            
            var noSuchMerch = await _client.IssueMerchToEmployee(1, "Wrong merch", CancellationToken.None);
            Assert.AreEqual(IssueMerchResponse.NoSuchMerch, noSuchMerch.IssueMerchResponse);
            
            var validMerchIssue = await _client.IssueMerchToEmployee(2, "Starter pack", CancellationToken.None);
            Assert.AreEqual(IssueMerchResponse.Created, validMerchIssue.IssueMerchResponse);
        }
        
        [Test]
        public async Task TestGiveOutPreparedPack()
        {
            var response = await _client.GiveOutPreparedPack(1, "Welcome pack", CancellationToken.None);
            Assert.AreEqual(GiveOutPreparedPackStatus.Ok, response.Status);

            Assert.ThrowsAsync<MerchPackNotPreparedException>(
                async () => await _client.GiveOutPreparedPack(1, "Non-existing pack", CancellationToken.None));
        }
        
        [Test]
        public async Task TestProcessNewSupplyArrival()
        {
            var response = await _client.ProcessNewSupplyArrival(new(){1, 2, 3}, CancellationToken.None);
            Assert.AreEqual(ProcessNewSupplyArrivalStatus.Ok, response.Status);
            
            // TODO: Implement and test error handling
        }
    }
}