using MediatR;
using Microsoft.AspNetCore.Mvc;
using RustRetail.IdentityService.API.Common;
using RustRetail.IdentityService.Application.Authentication.Login;
using RustRetail.IdentityService.Contracts.Authentication.Login;
using RustRetail.SharedInfrastructure.MinimalApi;

namespace RustRetail.IdentityService.API.Endpoints.V1.Authentication
{
    public class Login : IEndpoint
    {
        const string Route = $"{Resource.Authentication}/login";

        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost(Route, Handle)
                .WithTags(Tags.Authentication)
                .AllowAnonymous()
                .MapToApiVersion(1)
                .AddEndpointFilter<ValidationFilter<LoginRequest>>();
        }

        static async Task<IResult> Handle(
            [FromBody] LoginRequest? request,
            ISender sender,
            HttpContext httpContext,
            CancellationToken cancellationToken)
        {
            var loginCommand = new LoginCommand(request!.Email, request.Password);
            var result = await sender.Send(loginCommand, cancellationToken);
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
        }
    }
}
