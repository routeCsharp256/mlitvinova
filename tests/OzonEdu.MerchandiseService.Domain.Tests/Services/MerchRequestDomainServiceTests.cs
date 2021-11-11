using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Commands.CreateMerchRequest;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Interfaces;
using OzonEdu.MerchandiseService.Infrastructure.Exceptions;
using OzonEdu.MerchandiseService.Infrastructure.Handlers.EmployeeAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Queries.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Stubs;
using Xunit;

namespace OzonEdu.MerchandiseService.Domain.Tests.Services
{
    public class MerchRequestDomainServiceTests
    {
        private readonly IMerchRequestDomainService _service;
        private readonly Mock<IMediator> _mediator;

        public MerchRequestDomainServiceTests()
        {
            var stubData = new StubData();
            _mediator = new Mock<IMediator>();

            var merchPackRepo = new StubMerchPackRepository();
            var stockItemRepo = new StubStockItemRepository(stubData);
            var merchPackRequestRepo = new StubMerchRequestRepository(stubData);
            var merchPackRequestHistoryEntryRepo = new StubMerchPackRequestHistoryEntryRepository(stubData);

            var merchRequestFactory = new MerchPackRequestFactory(merchPackRepo);

            var createMerchRequestEntryCommandHandler = new CreateMerchRequestEntryCommandHandler(merchPackRequestRepo);
            var getMerchRequestsByEmployeeQueueHandler =
                new GetMerchRequestsByEmployeeQueueHandler(merchPackRequestRepo, merchPackRequestHistoryEntryRepo);

            _mediator
                .Setup(m => m.Publish(It.IsAny<CreateMerchRequestEntryCommand>(), It.IsAny<CancellationToken>()))
                .Callback<object, CancellationToken>((notification, cToken) =>
                    createMerchRequestEntryCommandHandler.Handle(notification as CreateMerchRequestEntryCommand,
                        cToken));

            _mediator
                .Setup(m => m.Send(It.IsAny<GetMerchRequestsByEmployeeQueue>(), It.IsAny<CancellationToken>()))
                .Returns<object, CancellationToken>((notification, cToken) =>
                    getMerchRequestsByEmployeeQueueHandler.Handle(notification as GetMerchRequestsByEmployeeQueue,
                        cToken));

            var merchPackFulfiller = new MerchRequestFulfiller(stockItemRepo, _mediator.Object, merchRequestFactory);

            _service = new MerchRequestDomainService(
                merchPackRepo,
                merchPackFulfiller,
                merchPackRequestHistoryEntryRepo,
                _mediator.Object);
        }
        
        [Fact]
        public async Task GiveOutMerch_CorrectMerchRequest_ShouldExecuteSuccessfully()
        {
            await _service.GiveOutMerch(
                3, "Starter pack", new() { }, CancellationToken.None);
        }

        [Fact]
        public async Task GiveOutMerch_MerchAlreadyIssued_ShouldThrow()
        {
            await Assert.ThrowsAsync<MerchAlreadyIssuedException>(() =>
                _service.GiveOutMerch(1, "Starter pack", new() { }, CancellationToken.None));
        }
    }
}