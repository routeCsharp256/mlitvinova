using System;
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
        
        public async Task<GetMerchPackDetailsResponse> V1GetMerchPackDetails(string name, CancellationToken token)
        {
            var normalizedUrl =  Uri.EscapeUriString($"/v1/api/merch/GetMerchPackContent?merchPackName={name}");
            using var response = await _httpClient.GetAsync(normalizedUrl, token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<GetMerchPackDetailsResponse>(body);
        }
        
        public async Task<GetMerchPackIssuedToEmployeeResponse> V1GetMerchIssuedToEmployee(long employeeId, CancellationToken token)
        {
            using var response = await _httpClient.GetAsync($"/v1/api/merch/GetMerchIssuedToEmployee?employeeId={employeeId}", token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<GetMerchPackIssuedToEmployeeResponse>(body);
        }
        
        public async Task<IssueMerchToEmployeeResponse> V1IssueMerchToEmployee(long employeeId, string merchName,
            CancellationToken token)
        {
            using var response = await _httpClient.PostAsync(
                $"/v1/api/merch/IssueMerchToEmployee?employeeId={employeeId}&merchPackName={merchName}", 
                null,
                token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<IssueMerchToEmployeeResponse>(body);
        }
    }
}