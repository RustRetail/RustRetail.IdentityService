using Microsoft.AspNetCore.Http.Features;
using RustRetail.IdentityService.API.Middlewares;
using System.Diagnostics;
using RustRetail.IdentityService.API.Configuration.Authentication;
using RustRetail.IdentityService.API.Configuration.Authorization;

namespace RustRetail.IdentityService.API.Configuration
{
    internal static class ServicesConfiguration
    {
        internal static IServiceCollection AddApi(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddApiAuthentication(configuration)
                .AddApiAuthorization()
                .AddGlobalExceptionHandling();

            return services;
        }

        private static IServiceCollection AddGlobalExceptionHandling(
            this IServiceCollection services)
        {
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails(options =>
            {
                options.CustomizeProblemDetails = (context) =>
                {
                    context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
                    context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
                    Activity? activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                    context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
                };
            });

            return services;
        }
    }
}
