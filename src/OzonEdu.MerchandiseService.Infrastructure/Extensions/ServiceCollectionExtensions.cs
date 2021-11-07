using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OzonEdu.MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateMerchRequestHistoryEntryCommandHandler).Assembly);
            
            return services;
        }
        
        public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
        {
            return services;
        }
    }
}