using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RustRetail.IdentityService.Application.Abstractions.Authentication;
using RustRetail.IdentityService.Infrastructure.Authentication.Jwt;
using RustRetail.IdentityService.Infrastructure.Authentication.Password;

namespace RustRetail.IdentityService.Infrastructure.Authentication
{
    public static class AuthenticationServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Password hasher
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

            // Jwt
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
            services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();

            return services;
        }
    }
}
