using System.Threading;
using System.Threading.Tasks;

namespace OzonEdu.MerchandiseService.Domain.Contracts
{
    public interface IRepository<TAggregationRoot>
    {
        IUnitOfWork UnitOfWork { get; }

        Task<TAggregationRoot> CreateAsync(TAggregationRoot itemToCreate,
            CancellationToken cancellationToken = default);

        Task<TAggregationRoot> UpdateAsync(TAggregationRoot itemToUpdate,
            CancellationToken cancellationToken = default);
    }
}