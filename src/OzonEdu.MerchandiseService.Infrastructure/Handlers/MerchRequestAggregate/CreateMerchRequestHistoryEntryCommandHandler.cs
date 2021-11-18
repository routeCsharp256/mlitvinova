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
        private readonly IMerchPackRequestHistoryEntryRepository _merchPackRequestHistoryEntryRepository;
        private readonly IMerchPackRequestRepository _merchPackRequestRepository;

        public CreateMerchRequestHistoryEntryCommandHandler(
            IMerchPackRequestHistoryEntryRepository merchPackRequestHistoryEntryRepository,
            IMerchPackRequestRepository merchPackRequestRepository)
        {
            _merchPackRequestHistoryEntryRepository = merchPackRequestHistoryEntryRepository ??
                                                      throw new ArgumentNullException(
                                                          $"{nameof(merchPackRequestHistoryEntryRepository)}");
            _merchPackRequestRepository = merchPackRequestRepository ??
                                          throw new ArgumentNullException($"{nameof(merchPackRequestRepository)}");
        }

        public async Task<Unit> Handle(CreateMerchRequestHistoryEntryCommand request, CancellationToken token)
        {
            var employee = new Employee(request.EmployeeId);
            var name = new MerchPackName(request.MerchPackName);

            var merchPackRequest = new MerchPackRequestHistoryEntry(
                employee,
                name,
                request.Sku.Select(it => new Sku(it)).ToList(),
                request.CompletedAt);

            var issuingRequest = (await _merchPackRequestRepository.FindByEmployeeAsync(employee, token))
                .FirstOrDefault(x => x.MerchPack.Name.Equals(name));
            if (issuingRequest is not null)
            {
                await _merchPackRequestRepository.DeleteAsync(issuingRequest, token);
            }

            await _merchPackRequestHistoryEntryRepository.CreateAsync(merchPackRequest, token);
            await _merchPackRequestHistoryEntryRepository.UnitOfWork.SaveChangesAsync(token);

            return Unit.Value;
        }
    }
}