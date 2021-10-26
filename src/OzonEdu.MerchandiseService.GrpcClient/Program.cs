using System;
using Grpc.Net.Client;
using OzonEdu.StockApi.Grpc;

using var channel = GrpcChannel.ForAddress("https://localhost:5001");
var client = new MerchandiseApiGrpc.MerchandiseApiGrpcClient(channel);

var test = client.GetEmployeeIssuedMerchStreaming(
    new GetMerchIssuedToEmployeeRequest()
    {
        EmployeeId = 1
    });


// var response = await client.GetAllStockItemsAsync(new GetAllStockItemsRequest(), cancellationToken: CancellationToken.None);
// foreach (var item in response.Stocks)
// {
//     Console.WriteLine($"item id {item.ItemId} - quantity {item.Quantity}");
// }