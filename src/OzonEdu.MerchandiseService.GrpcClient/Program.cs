using System;
using System.Threading;
using Grpc.Core;
using Grpc.Net.Client;
using OzonEdu.StockApi.Grpc;

using var channel = GrpcChannel.ForAddress("https://localhost:5001");
var client = new MerchandiseApiGrpc.MerchandiseApiGrpcClient(channel);

var clientStreamingCall = client.IssueMerchToEmployeeStreaming(cancellationToken: CancellationToken.None);
await clientStreamingCall.RequestStream.WriteAsync(new IssueMerchToEmployeeRequest
{
    EmployeeId = 1,
    MerchPackName = "Starter pack"
});

await clientStreamingCall.RequestStream.WriteAsync(new IssueMerchToEmployeeRequest
{
    EmployeeId = 1,
    MerchPackName = "Welcome pack"
});

var allClientMerchStream = client.GetEmployeeIssuedMerchStreaming(
new GetMerchIssuedToEmployeeRequest()
{
    EmployeeId = 1
});

await foreach (var pack in allClientMerchStream.ResponseStream.ReadAllAsync())
{
    Console.WriteLine($"Merch packs");
    foreach (var item in pack.Merch)
    {
        Console.WriteLine($"Pack - {item.MerchPackName}, Status - {item.Status}");
    }
}


// var response = await client.GetAllStockItemsAsync(new GetAllStockItemsRequest(), cancellationToken: CancellationToken.None);
// foreach (var item in response.Stocks)
// {
//     Console.WriteLine($"item id {item.ItemId} - quantity {item.Quantity}");
// }