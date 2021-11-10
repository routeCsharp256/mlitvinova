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
using OzonEdu.MerchandiseService.Infrastructure.Extensions;

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