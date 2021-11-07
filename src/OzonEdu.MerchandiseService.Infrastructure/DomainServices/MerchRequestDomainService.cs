using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Exceptions;
using OzonEdu.MerchandiseService.Infrastructure.Extensions;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainServices
{
    public class MerchRequestDomainService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMerchPackRepository _merchPackRepository;
        
        public MerchRequestDomainService(
            IEmployeeRepository employeeRepository,
            IMerchPackRepository merchPackRepository)
        {
            _employeeRepository = employeeRepository;
            _merchPackRepository = merchPackRepository;
        }
        
        public async Task GiveOutMerch(long employeeId, string merchPackName, Dictionary<string, string> constraints, CancellationToken token)
        {
            var name = new MerchPackName(merchPackName);
            var merchPackExists = await _merchPackRepository.MerchPackExists(name, token);
            if (!merchPackExists)
            {
                throw new MerchPackNotFoundException($"Not found: {merchPackName}");
            }

            var merchPack = await _merchPackRepository.GetMerchPackByName(name, token);
            
            var employee = new Employee(employeeId);
            var isMerchAlreadyReceived = await _employeeRepository.IsMerchAlreadyReceived(employee, name, token);
            if (isMerchAlreadyReceived)
            {
                throw new MerchAlreadyIssuedException($"Merch {merchPackName} already issued to employee {employeeId}");
            }

            var constraintEntities = new List<IMerchConstraint>();
            foreach (var constraint in constraints)
            {
                constraintEntities.Add(ConstraintConstructor.ConstructConstraint(constraint.Key, constraint.Value));
            }

            var merchRequest = new MerchPackRequest(employee, merchPack, constraintEntities);
        }
    }
}