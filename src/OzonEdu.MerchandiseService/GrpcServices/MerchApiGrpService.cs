using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MerchandiseService.Models;
using MerchandiseService.Services.Interfaces;
using OzonEdu.StockApi.Grpc;
using MerchItem = MerchandiseService.Models.MerchItem;
using MerchPack = OzonEdu.StockApi.Grpc.MerchPack;

namespace MerchandiseService.GrpcServices
{
    public class MerchApiGrpService : MerchandiseApiGrpc.MerchandiseApiGrpcBase
    {
        private readonly IMerchandiseService _service;

        public MerchApiGrpService(IMerchandiseService service)
        {
            _service = service;
        }

        public override async Task<Empty> IssueMerchToEmployeeStreaming(
            IAsyncStreamReader<IssueMerchToEmployeeRequest> requestStream, ServerCallContext context)
        {            
            while (!context.CancellationToken.IsCancellationRequested)
            {
                await requestStream.MoveNext();
                var currentItem = requestStream.Current;

                var items = currentItem.Pack.Items
                    .Select(x =>
                        new MerchItem(
                            x.MerchName, 
                            x.Properties.ToDictionary(y => y.PropertyName, y => y.PropertyValue)))
                    .ToList();
                
                await _service.IssueMerchToEmployee(currentItem.EmployeeId, new Models.MerchPack(items), context.CancellationToken);
            }

            return new Empty();
        }

        public override async Task GetEmployeeIssuedMerchStreaming(
            GetMerchIssuedToEmployeeRequest request,
            IServerStreamWriter<GetMerchIssuedToEmployeeResponse> responseStream,
            ServerCallContext context)
        {
            var items = await _service.GetIssuedMerchToEmployee(request.EmployeeId, context.CancellationToken);
            foreach (var item in items)
            {
                if (context.CancellationToken.IsCancellationRequested)
                {
                    break;
                }

                var status = item.Item2 switch
                {
                    MerchPurchaseStatus.Issued => MerchPackStatus.Issued,
                    MerchPurchaseStatus.Issuing => MerchPackStatus.Issuing,
                    _ => throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid status"))
                };

                var pack = new MerchPack()
                {
                    Items =
                    {
                        item.Item1.PackItems.Select(x => new OzonEdu.StockApi.Grpc.MerchItem
                        {
                            MerchName = x.Name,
                            Properties = { x.Properties.Select(y => new Property()
                            {
                                PropertyName = y.Key,
                                PropertyValue = y.Value
                            }) }
                        })
                    }
                };

                await responseStream.WriteAsync(new GetMerchIssuedToEmployeeResponse()
                {
                    Merch =
                    {
                        new MerchPackInStatus()
                        {
                            Pack = pack,
                            Status = status
                        }
                    }
                });
            }
        }
    }
}