using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RustRetail.IdentityService.API.Common;
using RustRetail.IdentityService.Application.Authentication.Register;
using RustRetail.IdentityService.Contracts.Authentication.Register;
using RustRetail.SharedInfrastructure.MinimalApi;

namespace RustRetail.IdentityService.API.Endpoints.V1.Authentication
{
    public class Register : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{Resource.Authentication}/register",
                async ([FromBody] RegisterRequest? request, HttpContext httpContext, ISender sender, CancellationToken cancellationToken) =>
                {
                    var command = new RegisterCommand(
                        request!.Email,
                        request.Password,
                        request.ConfirmPassword,
                        request.UserName);
                    var result = await sender.Send(command, cancellationToken);
                    return result.IsSuccess
                        ? Results.Ok(new SuccessResultWrapper(result, httpContext))
                        : ResultExtension.HandleFailure(result, httpContext);
                })
                .WithTags(Tags.Authentication)
                .AllowAnonymous()
                .MapToApiVersion(1)
                .AddEndpointFilter<ValidationFilter<RegisterRequest>>();
        }
    }

    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
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

            RuleFor(x => x.ConfirmPassword)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("Confirm Password is required and must be at least 6 characters long.")
                .Equal(x => x.Password)
                .WithMessage("Confirm Password must match password.");

            RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("Username is required and must be at least 6 characters long.")
                .MaximumLength(24)
                .WithMessage("Username cannot exceed 24 characters.")
                .Matches("^[a-zA-Z0-9_]+$")
                .WithMessage("Username can only contain alphanumeric characters and underscores.");
        }
    }
}
