namespace RustRetail.IdentityService.API.Configuration.Authentication.Cors
{
    internal static class CorsServiceCollectionExtensions
    {
        const string AllowedAllOriginsPolicy = "AllowAllOrigins";

        internal static IServiceCollection ConfigureCors(
            this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(AllowedAllOriginsPolicy, policy =>
                {
                    policy.AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials()
                          .SetIsOriginAllowed(_ => true);
                });
            });

            return services;
        }
    }
}
