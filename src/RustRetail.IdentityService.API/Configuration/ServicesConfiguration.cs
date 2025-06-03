using RustRetail.IdentityService.Infrastructure.Authentication;

namespace RustRetail.IdentityService.API.Configuration
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddAPI(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(configuration);

            return services;
        }
    }
}
