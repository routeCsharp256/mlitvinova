using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using Grpc.Core;
using Grpc.Net.Client;
using OzonEdu.StockApi.Grpc;

HttpClient.DefaultProxy = new WebProxy();

using var channel = GrpcChannel.ForAddress("https://localhost:5001");
var client = new MerchandiseApiGrpc.MerchandiseApiGrpcClient(channel);

try
{
    await client.IssueMerchToEmployeeAsync(
        new IssueMerchToEmployeeRequest
        {
            EmployeeId = 1,
            MerchPackName = "Starter pack"
        },
        cancellationToken: CancellationToken.None);
}
catch (RpcException e)
{
    Console.WriteLine($"Failed to add: {e.Status}");
    var metadata = e.Trailers;
    var error = metadata.FirstOrDefault(x => x.Key == "Error");
    Console.WriteLine(error);
}

try
{
    await client.IssueMerchToEmployeeAsync(
        new IssueMerchToEmployeeRequest
        {
            EmployeeId = 1,
            MerchPackName = "Welcome pack"
        },
        cancellationToken: CancellationToken.None);
}
catch (RpcException e)
{
    Console.WriteLine($"Failed to add: {e.Status}");
    var metadata = e.Trailers;
    var error = metadata.FirstOrDefault(x => x.Key == "Error");
    Console.WriteLine(error);
}

var allClientMerchStream = client.GetMerchIssuedToEmployeeStreaming(
    new GetMerchIssuedToEmployeeRequest()
    {
        EmployeeId = 1
    });

Console.WriteLine($"Merch packs");
await foreach (var pack in allClientMerchStream.ResponseStream.ReadAllAsync())
{
    foreach (var item in pack.Merch)
    {
        Console.WriteLine($"Pack - {item.MerchPackName}, Status - {item.Status}");
    }
}