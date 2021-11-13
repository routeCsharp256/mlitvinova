using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestHistoryEntryAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Queries.MerchRequestAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.Handlers.EmployeeAggregate
{
    public class GetMerchRequestsByEmployeeQueueHandler 
        : IRequestHandler<GetMerchRequestsByEmployeeQueue, List<MerchIssuedToEmployee>>
    {
        private readonly IMerchPackRequestRepository _merchPackRequestRepository;
        private readonly IMerchPackRequestHistoryEntryRepository _merchPackRequestHistoryEntryRepository;

        public GetMerchRequestsByEmployeeQueueHandler(IMerchPackRequestRepository merchPackRequestRepository,
            IMerchPackRequestHistoryEntryRepository merchPackRequestHistoryEntryRepository)
        {
            _merchPackRequestRepository = merchPackRequestRepository;
            _merchPackRequestHistoryEntryRepository = merchPackRequestHistoryEntryRepository;
        }
        
        public async Task<List<MerchIssuedToEmployee>> Handle(
            GetMerchRequestsByEmployeeQueue request, CancellationToken cancellationToken)
        {
            var finishedRequests = (await _merchPackRequestHistoryEntryRepository.FindByEmployeeAsync(
                    new Employee(request.EmployeeId),
                    cancellationToken))
                .Select(x => new MerchIssuedToEmployee(
                    x.MerchPackName, x.Status.ToMerchPackRequestStatus()));

            var unfinishedRequests = (await _merchPackRequestRepository.FindByEmployeeAsync(
                    new Employee(request.EmployeeId),
                    cancellationToken))
                .Select(x => new MerchIssuedToEmployee(
                    x.MerchPack.Name, MerchPackRequestStatus.WaitingForSupplies));

            return finishedRequests.Concat(unfinishedRequests).ToList();
        }
    }
}