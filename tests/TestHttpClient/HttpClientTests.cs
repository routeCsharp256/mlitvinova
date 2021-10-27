using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OzonEdu.MerchandiseService.HttpClient;

namespace TestHttpClient
{
    public class Tests
    {
        private MerchandiseHttpClient _client;
        
        [SetUp]
        public void Setup()
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:5001")
            };
            _client = new MerchandiseHttpClient(httpClient);
        }

        [Test]
        public async Task TestGetMerchDetails()
        {
            var nonexistentPack = await _client.V1GetMerchPackDetails("123123", CancellationToken.None);
            
            Assert.IsNull(nonexistentPack.PackName);
            Assert.IsNull(nonexistentPack.Items);
            
            var existingPack = await _client.V1GetMerchPackDetails("Starter pack", CancellationToken.None);
            
            Assert.AreEqual("Starter pack", existingPack.PackName);
            Assert.IsNotNull(existingPack.Items);
        }
        
        [Test]
        public async Task TestGetMerchIssued()
        {
            var employeeWithoutMerch = await _client.V1GetMerchIssuedToEmployee(2, CancellationToken.None);
            
            Assert.AreEqual(0, employeeWithoutMerch.MerchList.Count);
            
            var employeeWithMerch = await _client.V1GetMerchIssuedToEmployee(1, CancellationToken.None);
            
            Assert.AreEqual(1, employeeWithMerch.MerchList.Count);
        }
    }
}