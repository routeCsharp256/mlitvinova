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
        
        // public async Task<GetMerchPackIssuedToEmployeeResponse> V1GetMerchIssuedToEmployee(long employeeId, CancellationToken token)
        // {
        //     using var response = await _httpClient.GetAsync($"v1/api/GetMerchIssuedToEmployee?employeeId={employeeId}", token);
        //     var body = await response.Content.ReadAsStringAsync(token);
        //     return JsonSerializer.Deserialize<GetMerchPackIssuedToEmployeeResponse>(body);
        // }
    }
}