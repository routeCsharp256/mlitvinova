using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Queries.MerchRequestAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.Handlers.EmployeeAggregate
{
    public class GetMerchRequestsByEmployeeQueueHandler 
        : IRequestHandler<GetMerchRequestsByEmployeeQueue, List<MerchIssuedToEmployee>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetMerchRequestsByEmployeeQueueHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        
        public async Task<List<MerchIssuedToEmployee>> Handle(
            GetMerchRequestsByEmployeeQueue request, CancellationToken cancellationToken)
        {
            var requests = await _employeeRepository.GetMerchIssuedToEmployee(
                new Employee(request.EmployeeId),
                cancellationToken);

            return requests;
        }
    }
}