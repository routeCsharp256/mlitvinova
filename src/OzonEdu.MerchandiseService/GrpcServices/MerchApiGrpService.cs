using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Interfaces;
using OzonEdu.MerchandiseService.Infrastructure.Exceptions;
using OzonEdu.StockApi.Grpc;

namespace OzonEdu.MerchandiseService.GrpcServices
{
    public class MerchApiGrpService : MerchandiseApiGrpc.MerchandiseApiGrpcBase
    {
        private readonly IMerchRequestDomainService _merchRequestDomainService;
        
        public MerchApiGrpService(IMerchRequestDomainService merchRequestDomainService)
        {
            _merchRequestDomainService = merchRequestDomainService;
        }

        public override async Task<Empty> IssueMerchToEmployee(IssueMerchToEmployeeRequest request,
            ServerCallContext context)
        {
            void ReportError(Exception error) => 
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"Request initiation failed: {error}"), 
                    new Metadata {new Metadata.Entry("Error", error.ToString())});

            try
            {
                await _merchRequestDomainService.GiveOutMerch(
                    request.EmployeeId, 
                    request.MerchPackName, 
                    request.Constraints.ToDictionary(x => x.PropertyName, x => x.PropertyValue),
                    context.CancellationToken);
            }
            catch (MerchPackNotFoundException e)
            {
                ReportError(e);
            }
            catch (MerchAlreadyIssuedException e)
            {
                ReportError(e);
            }
            
            return new Empty();
        }

        public override async Task GetMerchIssuedToEmployeeStreaming(
            GetMerchIssuedToEmployeeRequest request,
            IServerStreamWriter<GetMerchIssuedToEmployeeResponse> responseStream,
            ServerCallContext context)
        {
            var merchIssued = await _merchRequestDomainService.GetMerchIssuedToEmployee(
                request.EmployeeId, context.CancellationToken);
            
            foreach (var item in merchIssued)
            {
                if (context.CancellationToken.IsCancellationRequested)
                {
                    break;
                }
            
                var status = item.Status switch
                {
                    var s when s.Equals(MerchPackRequestStatus.Completed) 
                        => MerchPackStatus.Completed,
                    var s when s.Equals(MerchPackRequestStatus.WaitingForEmployeeToTakeIt) 
                        => MerchPackStatus.WaitingForEmployeeToTakeIt,
                    var s when s.Equals(MerchPackRequestStatus.WaitingForSupplies) 
                        => MerchPackStatus.WaitingForSupplies,
                    _ => throw new ArgumentOutOfRangeException()
                };

                await responseStream.WriteAsync(new GetMerchIssuedToEmployeeResponse()
                {
                    Merch =
                    {
                        new MerchPackInStatus()
                        {
                            MerchPackName = item.Name.Value,
                            Status = status
                        }
                    }
                });
            }
        }

        public override async Task<GetMerchPackContentResponse> GetMerchPackContent(GetMerchPackContentRequest request,
            ServerCallContext context)
        {
            var merchPackDetails = await _merchRequestDomainService.GetMerchPackContent(
                request.MerchPackName, context.CancellationToken);
        
            if (merchPackDetails is null)
            {
                throw new RpcException(
                    new Status(StatusCode.InvalidArgument, $"Merch pack {request.MerchPackName} not found"));
            }

            return new GetMerchPackContentResponse
            {
                MerchPack = new MerchPack()
                {
                    MerchPackName = merchPackDetails.Name.Value,
                    PackItems =
                    {
                        merchPackDetails.MerchPackItems.Select(x => new MerchItem()
                        {
                            MerchName = x.ItemType.Name,
                            Properties =
                            {
                                x.Constraints.Select(y => new Property()
                                {
                                    PropertyName = y.Key(),
                                    PropertyValue = y.Value()
                                })
                            }
                        })
                    }
                }
            };
        }

        public override async Task<Empty> GiveOutPreparedPack(GiveOutPreparedPackRequest request,
            ServerCallContext context)
        {
            await _merchRequestDomainService.GiveOutPreparedPack(request.EmployeeId, request.MerchPackName, context.CancellationToken);

            return new Empty();
        }
        
        public override async Task<Empty> ProcessNewSupplyArrival(ProcessNewSupplyArrivalRequest request,
            ServerCallContext context)
        {
            var skus = request.SkuIds.ToList();
            await _merchRequestDomainService.ProcessNewSupplyArrival(skus, context.CancellationToken);

            return new Empty();
        }
    }
}