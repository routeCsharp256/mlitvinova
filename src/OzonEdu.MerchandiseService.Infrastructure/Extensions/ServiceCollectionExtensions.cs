using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackRequestHistoryEntryAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Interfaces;
using OzonEdu.MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Repositories.Implementation;
using OzonEdu.MerchandiseService.Infrastructure.Repositories.Implementation.Stubs;

namespace OzonEdu.MerchandiseService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateMerchRequestHistoryEntryCommandHandler).Assembly);

            services.AddScoped<MerchRequestFulfiller>();
            services.AddScoped<MerchPackRequestFactory>();
            services.AddScoped<IMerchRequestDomainService, MerchRequestDomainService>();
            
            return services;
        }
        
        public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            
            services.AddScoped<IStockItemRepository, StubStockItemRepository>();
            services.AddScoped<IMerchPackRepository, MerchPackRepository>();
            services.AddScoped<IMerchPackRequestRepository, StubMerchRequestRepository>();
            services.AddScoped<IMerchPackRequestHistoryEntryRepository, StubMerchPackRequestHistoryEntryRepository>();
            
            services.AddScoped<StubData>();
            
            return services;
        }
    }
}