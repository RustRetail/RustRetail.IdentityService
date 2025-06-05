using RustRetail.IdentityService.Domain.Constants;

namespace RustRetail.IdentityService.API.Configuration.Authorization
{
    internal static class AuthorizationServiceCollectionExtensions
    {
        internal static IServiceCollection AddApiAuthorization(
            this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                // Administrator
                options.AddPolicy(AuthorizationPolicy.AdministratorPolicy, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole(ApplicationRole.Administrator);
                });
                // User
                options.AddPolicy(AuthorizationPolicy.UserPolicy, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole(ApplicationRole.User);
                });
                // Authenticated user
                options.AddPolicy(AuthorizationPolicy.AuthenticatedUserPolicy, policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
            });

            return services;
        }
    }
}
