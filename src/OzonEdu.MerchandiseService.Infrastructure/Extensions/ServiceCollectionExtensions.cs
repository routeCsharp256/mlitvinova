using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices;
using OzonEdu.MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Stubs;

namespace OzonEdu.MerchandiseService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateMerchRequestHistoryEntryCommandHandler).Assembly);

            services.AddSingleton<MerchRequestFulfiller>();
            services.AddSingleton<MerchRequestDomainService>();
            
            return services;
        }
        
        public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IStockItemRepository, StubStockItemRepository>();
            services.AddSingleton<IMerchPackRepository, StubMerchPackRepository>();
            services.AddSingleton<IMerchPackRequestRepository, StubMerchRequestRepository>();

            services.AddSingleton<StubData>();
            
            return services;
        }
    }
}