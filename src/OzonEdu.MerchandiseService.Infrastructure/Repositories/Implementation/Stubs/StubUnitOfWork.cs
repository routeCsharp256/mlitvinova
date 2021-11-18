using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.Contracts;

namespace OzonEdu.MerchandiseService.Infrastructure.Repositories.Implementation.Stubs
{
    public class StubUnitOfWork : IUnitOfWork
    {
        public ValueTask StartTransaction(CancellationToken token)
        {
            return ValueTask.CompletedTask;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}