using FluentValidation;
using RustRetail.IdentityService.Contracts.Authentication.Register;

namespace RustRetail.IdentityService.API.Validators.V1.Authentication
{
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
