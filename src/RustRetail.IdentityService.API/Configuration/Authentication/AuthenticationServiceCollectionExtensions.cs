using Microsoft.AspNetCore.Authentication.JwtBearer;
using RustRetail.IdentityService.API.Configuration.Authentication.Jwt;

namespace RustRetail.IdentityService.API.Configuration.Authentication
{
    internal static class AuthenticationServiceCollectionExtensions
    {
        internal static IServiceCollection AddApiAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Jwt
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
            services.ConfigureOptions<JwtBearerOptionsSetup>();

            return services;
        }
    }
}
