using FluentValidation;
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
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{Resource.Authentication}/login", async ([FromBody] LoginRequest? request, ISender sender, HttpContext httpContext) =>
            {
                var loginCommand = new LoginCommand(request!.Email, request.Password);
                var result = await sender.Send(loginCommand);

                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : ResultExtension.HandleFailure(result, httpContext);
            })
            .WithTags(Tags.Authentication)
            .AllowAnonymous()
            .MapToApiVersion(1)
            .AddEndpointFilter<ValidationFilter<LoginRequest>>();
        }
    }

    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email is required and must be a valid email address.");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("Password is required and must be at least 6 characters long.");
        }
    }
}
