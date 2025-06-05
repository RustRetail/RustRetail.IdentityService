using RustRetail.SharedInfrastructure.MinimalApi;

namespace RustRetail.IdentityService.API.Endpoints.V1.Authentication
{
    public class Login : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{Resource.Authentication}/login", () =>
            {
                return "Access login endpoint";
            })
            .WithTags(Tags.Authentication)
            .MapToApiVersion(1);
        }
    }
}
