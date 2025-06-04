namespace RustRetail.IdentityService.API.Configuration
{
    public static class ApplicationConfiguration
    {
        internal static WebApplication ConfigureApplicationPipeline(
            this WebApplication app)
        {
            app.UseHttpsRedirection()
                .UseAuthentication()
                //.UseAuthorization()
                .UseExceptionHandler();

            return app;
        }
    }
}
