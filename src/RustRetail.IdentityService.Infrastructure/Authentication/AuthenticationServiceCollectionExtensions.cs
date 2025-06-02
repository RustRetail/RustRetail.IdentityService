using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RustRetail.IdentityService.Application.Abstractions.Authentication;
using RustRetail.IdentityService.Infrastructure.Authentication.Password;

namespace RustRetail.IdentityService.Infrastructure.Authentication
{
    internal static class AuthenticationServiceCollectionExtensions
    {
        internal static IServiceCollection AddAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Password hasher
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

            return services;
        }
    }
}
