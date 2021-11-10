using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestHistoryEntryAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Infrastructure.Commands.CreateMerchRequest;

namespace OzonEdu.MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    public class CreateMerchRequestHistoryEntryCommandHandler : IRequestHandler<CreateMerchRequestHistoryEntryCommand>
    {
        private readonly IMerchPackRequestHistoryEntryRepository _repository;

        public CreateMerchRequestHistoryEntryCommandHandler(IMerchPackRequestHistoryEntryRepository repository)
        {
            _repository = repository ?? 
                          throw new ArgumentNullException($"{nameof(repository)}");
        }

        public async Task<Unit> Handle(CreateMerchRequestHistoryEntryCommand request, CancellationToken token)
        {
            var merchPackRequest = new MerchPackRequestHistoryEntry(
                new Employee(request.EmployeeId),
                new MerchPackName(request.MerchPackName),
                request.Sku.Select(it => new Sku(it)).ToList(),
                request.CompletedAt);
            
            await _repository.CreateAsync(merchPackRequest, token);
            await _repository.UnitOfWork.SaveEntitiesAsync(token);
            
            return Unit.Value;
        }
    }
}