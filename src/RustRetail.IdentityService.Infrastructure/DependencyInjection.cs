using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RustRetail.IdentityService.Infrastructure.ApplicationServices;
using RustRetail.IdentityService.Infrastructure.Authentication;
using RustRetail.IdentityService.Infrastructure.MessageBrokers;

namespace RustRetail.IdentityService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(configuration);
            services.AddApplicationServices();
            services.AddMessaging(configuration);

            return services;
        }
    }
}
