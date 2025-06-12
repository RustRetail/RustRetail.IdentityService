using RustRetail.IdentityService.API.Common;
using RustRetail.IdentityService.Domain.Errors.Authentication;
using RustRetail.SharedInfrastructure.MinimalApi;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.API.Endpoints.V1.Authentication
{
    public class RevokeRefreshToken : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{Resource.Authentication}/revoke-token", async (HttpContext httpContext) =>
            {
                if(!httpContext.Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
                {
                    return ResultExtension.HandleFailure(Result.Failure(AuthenticationErrors.RefreshTokenCookieNotFound), httpContext);
                }

                throw new NotImplementedException();
            })
            .WithTags(Tags.Authentication)
            .AllowAnonymous()
            .MapToApiVersion(1);
        }
    }
}
