using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Interfaces;
using OzonEdu.MerchandiseService.Infrastructure.Exceptions;
using OzonEdu.MerchandiseService.Infrastructure.Extensions;
using OzonEdu.MerchandiseService.Infrastructure.Queries.MerchRequestAggregate;
using ConstraintConstructor = OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate.ConstraintConstructor;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainServices
{
    public class MerchRequestDomainService : IMerchRequestDomainService
    {
        private readonly IMerchPackRepository _merchPackRepository;
        private readonly MerchRequestFulfiller _merchRequestFulfiller;
        private readonly IMediator _mediator;

        public MerchRequestDomainService(
            IMerchPackRepository merchPackRepository,
            MerchRequestFulfiller merchRequestFulfiller,
            IMediator mediator)
        {
            _merchPackRepository = merchPackRepository;
            _merchRequestFulfiller = merchRequestFulfiller;
            _mediator = mediator;
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

            var merchPack = await _merchPackRepository.GetMerchPackByName(name, token);

            var employee = new Employee(employeeId);

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
    }
}