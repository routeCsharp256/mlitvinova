using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestHistoryEntryAggregate;
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
        private readonly IMerchPackRequestRepository _merchPackRequestRepo;
        private readonly IMerchPackRequestHistoryEntryRepository _merchPackRequestHistoryEntryRepo;

        public MerchRequestDomainServiceTests()
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
                _mediator.Object);
        }
        
        [Fact]
        public async Task GiveOutMerch_CorrectMerchRequest_ShouldExecuteSuccessfully()
        {
            var employeeId = 3;
            var merchPackName = "Starter pack";
            
            await _service.GiveOutMerch(
                employeeId, merchPackName, new() { }, CancellationToken.None);

            var repositoryContents = await _merchPackRequestHistoryEntryRepo.FindByEmployeeAsync(
                new Employee(employeeId),
                CancellationToken.None);

            Assert.Contains(repositoryContents, x => x.MerchPackName.Value.Equals(merchPackName));
        }

        [Fact]
        public async Task GiveOutMerch_MerchAlreadyIssued_ShouldThrow()
        {
            await Assert.ThrowsAsync<MerchAlreadyIssuedException>(() =>
                _service.GiveOutMerch(1, "Starter pack", new() { }, CancellationToken.None));
        }
    }
}