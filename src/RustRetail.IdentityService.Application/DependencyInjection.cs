using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RustRetail.SharedApplication.Behaviors.Logging;
using RustRetail.SharedApplication.Behaviors.Validation;
using FluentValidation;

namespace RustRetail.IdentityService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.ConfigureMediatR()
                .AddRequestLoggingBehavior()
                .AddRequestValidationBehavior();

            return services;
        }

        private static IServiceCollection ConfigureMediatR(
            this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });

            return services;
        }

        private static IServiceCollection AddRequestLoggingBehavior(
            this IServiceCollection services)
        {
            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(LoggingBehavior<,>));

            return services;
        }

        private static IServiceCollection AddRequestValidationBehavior(
            this IServiceCollection services)
        {
            // Validation behavior
            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));

            // Validators
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            return services;
        }
    }
}
