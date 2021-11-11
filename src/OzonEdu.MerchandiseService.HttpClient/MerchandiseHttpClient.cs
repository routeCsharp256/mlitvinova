using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.HttpModels;

namespace OzonEdu.MerchandiseService.HttpClient
{
    public class MerchandiseHttpClient : IMerchandiseHttpClient
    {
        private readonly System.Net.Http.HttpClient _httpClient;

        public MerchandiseHttpClient(System.Net.Http.HttpClient client)
        {
            _httpClient = client;
        }
        
        public async Task<GetMerchPackDetailsResponse> GetMerchPackContent(string name, CancellationToken token)
        {
            var normalizedUrl =  Uri.EscapeUriString($"/v1/api/merch/GetMerchPackContent?merchPackName={name}");
            using var response = await _httpClient.GetAsync(normalizedUrl, token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<GetMerchPackDetailsResponse>(body);
        }
        
        public async Task<GetMerchPackIssuedToEmployeeResponse> GetMerchIssuedToEmployee(long employeeId, CancellationToken token)
        {
            using var response = await _httpClient.GetAsync($"/v1/api/merch/GetMerchIssuedToEmployee?employeeId={employeeId}", token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<GetMerchPackIssuedToEmployeeResponse>(body);
        }

        public async Task<GiveOutPreparedPackResponse> GiveOutPreparedPack(int employeeId, string packName, CancellationToken token)
        {
            var payload = "{}";
            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
            
            using var response = await _httpClient.PostAsync(
                $"/v1/api/merch/GiveOutPreparedPack?employeeId={employeeId}&packName={packName}", 
                content,
                token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<GiveOutPreparedPackResponse>(body);
        }

        public async Task<ProcessNewSupplyArrivalResponse> ProcessNewSupplyArrival(List<long> skuArrived, CancellationToken token)
        {
            var payload = JsonSerializer.Serialize(skuArrived);
            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
            
            using var response = await _httpClient.PostAsync(
                $"/v1/api/merch/ProcessNewSupplyArrival", 
                content,
                token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<ProcessNewSupplyArrivalResponse>(body);
        }

        public async Task<IssueMerchToEmployeeResponse> IssueMerchToEmployee(long employeeId, string merchName,
            CancellationToken token)
        {
            var payload = "{}";
            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

            using var response = await _httpClient.PostAsync(
                $"/v1/api/merch/IssueMerchToEmployee?employeeId={employeeId}&merchPackName={merchName}", 
                content,
                token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<IssueMerchToEmployeeResponse>(body);
        }
    }
}