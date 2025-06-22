using Microsoft.Extensions.DependencyInjection;
using RustRetail.IdentityService.Application.Abstractions.Services;
using RustRetail.SharedApplication.Behaviors.Outbox;

namespace RustRetail.IdentityService.Infrastructure.ApplicationServices
{
    internal static class ApplicationServicesCollectionExtensions
    {
        internal static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IOutboxMessageService, IdentityOutboxMessageService>();
            return services;
        }
    }
}
