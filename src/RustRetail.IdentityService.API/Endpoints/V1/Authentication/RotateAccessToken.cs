using MediatR;
using RustRetail.IdentityService.API.Common;
using RustRetail.IdentityService.Application.Authentication.RotateAccessToken;
using RustRetail.IdentityService.Domain.Errors.Authentication;
using RustRetail.SharedInfrastructure.MinimalApi;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.API.Endpoints.V1.Authentication
{
    public class RotateAccessToken : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{Resource.Authentication}/rotate-token",
                async (HttpContext httpContext, ISender sender) =>
                {
                    if (!httpContext.Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
                    {
                        return ResultExtension.HandleFailure(Result.Failure(AuthenticationErrors.RefreshTokenCookieNotFound), httpContext);
                    }
                    var command = new RotateAccessTokenCommand(refreshToken);
                    var result = await sender.Send(command);

                    if (!result.IsSuccess)
                    {
                        return ResultExtension.HandleFailure(result, httpContext);
                    }

                    // Set refresh token as an HTTP-only, secure cookie
                    httpContext.Response.Cookies.Append("refreshToken", result.Value.RefreshToken, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        // Hard coded, to be modified later
                        Expires = DateTimeOffset.UtcNow.AddDays(7)
                    });

                    var resultWrapper = new SuccessResultWrapper<object>(result, httpContext, new { result.Value.AccessToken });
                    return Results.Ok(resultWrapper);
                })
                .WithTags(Tags.Authentication)
                .AllowAnonymous()
                .MapToApiVersion(1);
        }
    }
}
