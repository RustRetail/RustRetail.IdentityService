using FluentValidation;
using RustRetail.IdentityService.Contracts.Authentication.Login;

namespace RustRetail.IdentityService.API.Validators.V1.Authentication
{
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
