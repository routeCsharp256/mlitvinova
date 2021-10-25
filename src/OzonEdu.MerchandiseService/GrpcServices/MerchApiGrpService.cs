using MerchandiseService.Services.Interfaces;
using OzonEdu.StockApi.Grpc;

namespace MerchandiseService.GrpcServices
{
    public class MerchApiGrpService : MerchandiseApiGrpc.MerchandiseApiGrpcBase
    {
        private readonly IMerchandiseService _service;

        public MerchApiGrpService(IMerchandiseService service)
        {
            _service = service;
        }
        
        
    }
}