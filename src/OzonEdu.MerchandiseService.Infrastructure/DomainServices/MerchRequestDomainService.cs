using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestHistoryEntryAggregate;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Interfaces;
using OzonEdu.MerchandiseService.Infrastructure.Exceptions;
using OzonEdu.MerchandiseService.Infrastructure.Queries.MerchRequestAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainServices
{
    public class MerchRequestDomainService : IMerchRequestDomainService
    {
        private readonly IMerchPackRepository _merchPackRepository;
        private readonly IMerchPackRequestRepository _merchPackRequestRepository;
        private readonly IMerchPackRequestHistoryEntryRepository _merchPackRequestHistoryEntryRepository;
        
        private readonly MerchRequestFulfiller _merchRequestFulfiller;
        private readonly IMediator _mediator;

        public MerchRequestDomainService(
            IMerchPackRepository merchPackRepository,
            MerchRequestFulfiller merchRequestFulfiller,
            IMerchPackRequestHistoryEntryRepository merchPackRequestHistoryEntryRepository,
            IMerchPackRequestRepository merchPackRequestRepository,
            IMediator mediator)
        {
            _merchPackRepository = merchPackRepository;
            _merchRequestFulfiller = merchRequestFulfiller;
            _mediator = mediator;
            _merchPackRequestHistoryEntryRepository = merchPackRequestHistoryEntryRepository;
            _merchPackRequestRepository = merchPackRequestRepository;
        }

        public async Task GiveOutMerch(int employeeId, string merchPackName, Dictionary<string, string> constraints,
            CancellationToken token)
        {
            var name = new MerchPackName(merchPackName);
            var merchPackExists = await _merchPackRepository.MerchPackExists(name, token);
            if (!merchPackExists)
            {
                throw new MerchPackNotFoundException($"Not found: {merchPackName}");
            }

            var alreadyReceivedMerch = await _mediator.Send(
                new GetMerchRequestsByEmployeeQueue()
                {
                    EmployeeId = employeeId
                },
                token);
            var isMerchAlreadyReceived = alreadyReceivedMerch.Any(x => x.Name.Equals(name));

            if (isMerchAlreadyReceived)
            {
                throw new MerchAlreadyIssuedException($"Merch {merchPackName} already issued to employee {employeeId}");
            }

            await _merchRequestFulfiller.GiveOutMerchPack(employeeId, merchPackName, constraints, token);
        }

        public async Task<List<MerchIssuedToEmployee>> GetMerchIssuedToEmployee(int employeeId,
            CancellationToken token)
        {
            var alreadyReceivedMerch = await _mediator.Send(
                new GetMerchRequestsByEmployeeQueue()
                {
                    EmployeeId = employeeId
                },
                token);

            return alreadyReceivedMerch;
        }

        public async Task GiveOutPreparedPack(int employeeId, string packName, CancellationToken token)
        {
            var merchPackName = new MerchPackName(packName);
            var merchWaitingToBeGivenOut = await _merchPackRequestHistoryEntryRepository.FindByEmployeeAsync(
                new Employee(employeeId),
                token);

            var waitingRequest = merchWaitingToBeGivenOut.FirstOrDefault(x => x.MerchPackName.Equals(merchPackName));
            if (waitingRequest is null)
            {
                throw new MerchPackNotPreparedException($"Merch pack {packName} not prepared for employee {employeeId}");
            }
            
            waitingRequest.SetRequestToCompleted();
        }

        public async Task ProcessNewSupplyArrival(List<long> skuArrived, CancellationToken token)
        {
            var unfulfilledRequests = await _merchPackRequestRepository.FindAllAsync(token);
            var unmodified = new MerchPackRequest[unfulfilledRequests.Count];

            unfulfilledRequests.CopyTo(unmodified);
            
            foreach (var unfulfilledRequest in unmodified)
            {
                await _merchRequestFulfiller.GiveOutMerchPack(
                    unfulfilledRequest.EmployeeId.Id, 
                    unfulfilledRequest.MerchPack.Name.Value, 
                    unfulfilledRequest.PackConstraints.ToDictionary(x => x.Key(), x => x.Value()),
                    token);
            }
        }
        
        public async Task<MerchPack> GetMerchPackContent(string packName, CancellationToken token)
        {
            var merchPackName = new MerchPackName(packName);
            return await _merchPackRepository.GetMerchPackByName(merchPackName, token);
        }
    }
}