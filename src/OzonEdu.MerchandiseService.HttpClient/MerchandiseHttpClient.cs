using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.Models;

namespace OzonEdu.MerchandiseService.HttpClient
{
    public class MerchandiseHttpClient : IMerchandiseHttpClient
    {
        private readonly System.Net.Http.HttpClient _httpClient;

        public MerchandiseHttpClient(System.Net.Http.HttpClient client)
        {
            _httpClient = client;
        }
        
        public async Task<List<(MerchPack, MerchPurchaseStatus)>> V1GetAll(CancellationToken token)
        {
            using var response = await _httpClient.GetAsync("v1/api/merch", token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<List<(MerchPack, MerchPurchaseStatus)>>(body);
        }
    }
}