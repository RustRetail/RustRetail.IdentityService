using RustRetail.IdentityService.Application.Configuration.Authentication;
using RustRetail.IdentityService.Infrastructure.Authentication.Jwt;

namespace RustRetail.IdentityService.API.Configuration
{
    internal static class OptionsConfiguration
    {
        internal static IServiceCollection ConfiguringOptions(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Application
            services.Configure<AuthenticationSettingOptions>(configuration.GetSection(AuthenticationSettingOptions.SectionName));

            // Persistence

            // Infrastructure
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

            return services;
        }
    }
}
