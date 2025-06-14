using MediatR;
using RustRetail.IdentityService.API.Common;
using RustRetail.IdentityService.Application.Authentication.RevokeRefreshToken;
using RustRetail.IdentityService.Domain.Errors.Authentication;
using RustRetail.SharedInfrastructure.MinimalApi;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.API.Endpoints.V1.Authentication
{
    public class RevokeRefreshToken : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{Resource.Authentication}/revoke-token", async (HttpContext httpContext, ISender sender) =>
            {
                if(!httpContext.Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
                {
                    return ResultExtension.HandleFailure(Result.Failure(AuthenticationErrors.RefreshTokenCookieNotFound), httpContext);
                }

                var command = new RevokeRefreshTokenCommand(refreshToken);
                var result = await sender.Send(command);

                return result.IsSuccess
                    ? Results.Ok(new SuccessResultWrapper(result, httpContext))
                    : ResultExtension.HandleFailure(result, httpContext);
            })
            .WithTags(Tags.Authentication)
            .RequireAuthorization()
            .MapToApiVersion(1);
        }
    }
}
