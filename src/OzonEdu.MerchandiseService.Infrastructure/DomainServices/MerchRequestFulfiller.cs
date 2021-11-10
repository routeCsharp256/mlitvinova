using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestHistoryEntryAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.MerchandiseService.Domain.Exceptions;
using OzonEdu.MerchandiseService.Infrastructure.Commands.CreateMerchRequest;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainServices
{
    public class MerchRequestFulfiller
    {
        private readonly IStockItemRepository _stockItemRepository;
        private readonly IMediator _mediator;
        
        public MerchRequestFulfiller(IStockItemRepository repository, IMediator mediator)
        {
            _stockItemRepository = repository;
            _mediator = mediator;
        }

        public async Task GiveOutMerchPack(MerchPackRequest request, CancellationToken token)
        {
            var allStockItems = await _stockItemRepository.GetAllStockItems(token);

            var skuList = request.FilterByConstraints(allStockItems);

            var reservationSuccessful = await _stockItemRepository.GiveOutSkus(skuList, token);

            if (!reservationSuccessful)
            {
                await _mediator.Send(new CreateMerchRequestEntryCommand()
                    {
                        Request = request
                    },
                    token);
                // call for helb
            }
            else
            {
                await _mediator.Send(new CreateMerchRequestHistoryEntryCommand()
                {
                    CompletedAt = DateTime.Now,
                    EmployeeId = request.EmployeeId.Id,
                    MerchPackName = request.MerchPack.Name.Value,
                    Sku = skuList.Select(x => x.Value).ToList()
                });
            }
        }
    }
}