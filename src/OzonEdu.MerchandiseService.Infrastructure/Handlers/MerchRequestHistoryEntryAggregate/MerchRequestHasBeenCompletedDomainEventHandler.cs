using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestHistoryEntryAggregate;
using OzonEdu.MerchandiseService.Domain.Events;

namespace OzonEdu.MerchandiseService.Infrastructure.Handlers.MerchRequestHistoryEntryAggregate
{
    public class MerchRequestHasBeenCompletedDomainEventHandler : INotificationHandler<MerchRequestHasBeenCompletedDomainEvent>
    {
        private readonly IMerchPackRequestHistoryEntryRepository _repository;
        
        public MerchRequestHasBeenCompletedDomainEventHandler(IMerchPackRequestHistoryEntryRepository repository)
        {
            _repository = repository;
        }
        
        public async Task Handle(MerchRequestHasBeenCompletedDomainEvent notification, CancellationToken token)
        {
            await _repository.UpdateAsync(notification.Entry, token);
            await _repository.UnitOfWork.SaveEntitiesAsync(token);
        }
    }
}