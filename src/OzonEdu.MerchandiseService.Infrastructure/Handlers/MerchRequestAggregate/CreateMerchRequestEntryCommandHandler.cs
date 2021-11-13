using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Commands.CreateMerchRequest;

namespace OzonEdu.MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    public class CreateMerchRequestEntryCommandHandler : IRequestHandler<CreateMerchRequestEntryCommand>
    {
        private readonly IMerchPackRequestRepository _merchPackRequestRepository;

        public CreateMerchRequestEntryCommandHandler(
            IMerchPackRequestRepository merchPackRequestRepository)
        {
            _merchPackRequestRepository = merchPackRequestRepository ??
                                          throw new ArgumentNullException($"{nameof(merchPackRequestRepository)}");
        }

        public async Task<Unit> Handle(CreateMerchRequestEntryCommand request, CancellationToken token)
        {
            await _merchPackRequestRepository.CreateAsync(request.MerchPackRequest, token);
            await _merchPackRequestRepository.UnitOfWork.SaveEntitiesAsync(token);

            return Unit.Value;
        }
    }
}