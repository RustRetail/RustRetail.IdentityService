using Asp.Versioning.Builder;
using Asp.Versioning;
using RustRetail.SharedInfrastructure.MinimalApi;
using RustRetail.SharedInfrastructure.Logging.Serilog;

namespace RustRetail.IdentityService.API.Configuration
{
    internal static class ApplicationConfiguration
    {
        internal static WebApplication ConfigureApplicationPipeline(
            this WebApplication app)
        {
            app.UseAuthentication()
                .UseAuthorization();
            app.UseMinimalApiEndpoints();
            app.UseExceptionHandler();
            app.UseSharedSerilogRequestLogging();

            return app;
        }

        private static WebApplication UseMinimalApiEndpoints(
            this WebApplication app)
        {
            // Configure Api versioning
            ApiVersionSet apiVersionSet = app.NewApiVersionSet()
                .HasApiVersion(new ApiVersion(1.0))
                .ReportApiVersions()
                .Build();
            RouteGroupBuilder versionedGroup = app
                .MapGroup("api/v{version:apiVersion}")
                .WithApiVersionSet(apiVersionSet);

            app.MapEndpoints(versionedGroup);

            return app;
        }
    }
}
