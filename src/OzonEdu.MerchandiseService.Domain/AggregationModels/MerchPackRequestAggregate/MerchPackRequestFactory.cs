using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate
{
    public class MerchPackRequestFactory
    {
        private readonly IMerchPackRepository _repository;
        
        public MerchPackRequestFactory(IMerchPackRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<MerchPackRequest> BuildRequest(
            int employeeId,
            string merchPackName,
            Dictionary<string, string> constraints,
            CancellationToken token)
        {
            var name = new MerchPackName(merchPackName);
            var employee = new Employee(employeeId);
            
            var merchPack = await _repository.GetMerchPackByName(name, token);
            
            var constraintEntities = constraints
                .Select(constraint =>
                    ConstraintConstructor.ConstructConstraint(constraint.Key, constraint.Value))
                .ToList();

            return new MerchPackRequest(employee, merchPack, constraintEntities);
        }
    }
}