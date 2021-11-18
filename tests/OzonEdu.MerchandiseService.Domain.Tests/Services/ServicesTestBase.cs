using System.Threading;
using MediatR;
using Moq;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestHistoryEntryAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Commands.CreateMerchRequest;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Interfaces;
using OzonEdu.MerchandiseService.Infrastructure.Handlers.EmployeeAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Queries.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Repositories.Implementation.Stubs;

namespace OzonEdu.MerchandiseService.Domain.Tests.Services
{
    public class ServicesTestBase
    {
        protected readonly IMerchRequestDomainService _service;
        protected readonly Mock<IMediator> _mediator;
        protected readonly IMerchPackRequestRepository _merchPackRequestRepo;
        protected readonly IMerchPackRequestHistoryEntryRepository _merchPackRequestHistoryEntryRepo;

        public ServicesTestBase()
        {
            var stubData = new StubData();

            var merchPackRepo = new StubMerchPackRepository();
            var stockItemRepo = new StubStockItemRepository(stubData);
            _merchPackRequestRepo = new StubMerchRequestRepository(stubData);
            _merchPackRequestHistoryEntryRepo = new StubMerchPackRequestHistoryEntryRepository(stubData);

            _mediator = new Mock<IMediator>();
            
            var merchRequestFactory = new MerchPackRequestFactory(merchPackRepo);

            var createMerchRequestEntryCommandHandler = new CreateMerchRequestEntryCommandHandler(_merchPackRequestRepo);
            var createMerchRequestHistoryEntryCommandHandler =
                new CreateMerchRequestHistoryEntryCommandHandler(_merchPackRequestHistoryEntryRepo,
                    _merchPackRequestRepo);
            var getMerchRequestsByEmployeeQueueHandler =
                new GetMerchRequestsByEmployeeQueueHandler(_merchPackRequestRepo, _merchPackRequestHistoryEntryRepo);

            _mediator
                .Setup(m => m.Publish(It.IsAny<CreateMerchRequestEntryCommand>(), It.IsAny<CancellationToken>()))
                .Callback<object, CancellationToken>((notification, cToken) =>
                    createMerchRequestEntryCommandHandler.Handle(notification as CreateMerchRequestEntryCommand,
                        cToken));
            
            _mediator
                .Setup(m => m.Send(
                    It.IsAny<CreateMerchRequestHistoryEntryCommand>(), 
                    It.IsAny<CancellationToken>()))
                .Callback<object, CancellationToken>((notification, cToken) =>
                    createMerchRequestHistoryEntryCommandHandler.Handle(notification as CreateMerchRequestHistoryEntryCommand,
                        cToken));

            _mediator
                .Setup(m => m.Send(
                    It.IsAny<GetMerchRequestsByEmployeeQueue>(), 
                    It.IsAny<CancellationToken>()))
                .Returns<object, CancellationToken>((notification, cToken) =>
                    getMerchRequestsByEmployeeQueueHandler.Handle(notification as GetMerchRequestsByEmployeeQueue,
                        cToken));

            var merchPackFulfiller = new MerchRequestFulfiller(stockItemRepo, _mediator.Object, merchRequestFactory);

            _service = new MerchRequestDomainService(
                merchPackRepo,
                merchPackFulfiller,
                _merchPackRequestHistoryEntryRepo,
                _merchPackRequestRepo,
                _mediator.Object);
        }
    }
}