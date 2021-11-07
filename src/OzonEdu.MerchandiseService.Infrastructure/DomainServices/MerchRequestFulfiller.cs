using System;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.MerchandiseService.Domain.Exceptions;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainServices
{
    public class MerchRequestFulfiller
    {
        private readonly IStockItemRepository _stockItemRepository;
        private readonly IMerchPackRequestRepository _merchPackRequestRepository;
        
        public MerchRequestFulfiller(IStockItemRepository stockItemRepository, IMerchPackRequestRepository merchPackRequestRepository)
        {
            _stockItemRepository = stockItemRepository;
            _merchPackRequestRepository = merchPackRequestRepository;
        }

        public async Task GiveOutMerchPack(MerchPackRequest request, CancellationToken token)
        {
            var allStockItems = await _stockItemRepository.GetAllStockItems(token);

            var skuList = request.FilterByConstraints(allStockItems);

            var reservationSuccessful = await _stockItemRepository.GiveOutSkus(skuList, token);

            if (!reservationSuccessful)
            {
                await _merchPackRequestRepository.AddWaitingForCompletionRequest(request, token);
                // call for helb
            }
            else
            {
                var successfullyFulfilledRequest = new MerchPackRequestHistoryEntry(
                    request.EmployeeId,
                    request.MerchPack.Name,
                    skuList,
                    DateTime.Now);

                await _merchPackRequestRepository.AddSuccessfullyOrderedRequest(successfullyFulfilledRequest, token);
            }
        }
    }
}