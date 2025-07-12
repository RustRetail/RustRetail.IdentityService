using FluentValidation;
using RustRetail.IdentityService.Contracts.Users.CreateOrUpdateProfile;
using RustRetail.IdentityService.Domain.Enums;
using RustRetail.SharedKernel.Domain.Enums;

namespace RustRetail.IdentityService.API.Validators.V1.Users
{
    public class CreateOrUpdateUserProfileRequestValidator : AbstractValidator<CreateOrUpdateUserProfileRequest>
    {
        public CreateOrUpdateUserProfileRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                .WithMessage("First name is required.")
                .MaximumLength(100)
                .WithMessage("First name cannot exceed 100 characters.")
                .Matches("^\\p{L}(?:[\\p{L}'\\- ]*\\p{L})?$")
                .WithMessage("First name must start and end with a letter and may contain only letters, spaces, hyphens, or apostrophes.");

            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Last name is required.")
                .MaximumLength(100)
                .WithMessage("Last name cannot exceed 100 characters.")
                .Matches("^\\p{L}(?:[\\p{L}'\\- ]*\\p{L})?$")
                .WithMessage("Last name must start and end with a letter and may contain only letters, spaces, hyphens, or apostrophes.");

            RuleFor(x => x.Gender)
                .NotEmpty()
                .NotNull()
                .WithMessage("Gender is required")
                .Must(x => Enumeration.GetAll<Gender>().Any(g => g.Name.Equals(x, StringComparison.OrdinalIgnoreCase)))
                .WithMessage($"Gender must be one of: {string.Join(", ", Enumeration.GetAll<Gender>().Select(g => g.Name))}.");

            RuleFor(x => x.Bio)
                .MaximumLength(1024)
                .WithMessage("Bio cannot exceed 1024 characters.");

            RuleFor(x => x.DateOfBirth)
                .Must(BeAValidDateOrEmpty)
                .WithMessage("Date of birth must be a valid date in yyyy-MM-dd format and in the past.");
        }

        private bool BeAValidDateOrEmpty(string? dateOfBirth)
        {
            if (string.IsNullOrWhiteSpace(dateOfBirth))
                return true; // it's optional

            // Try parse
            if (DateTime.TryParse(dateOfBirth, out var parsed))
            {
                // Must be in the past
                return parsed.Date < DateTime.Today;
            }

            return false; // Invalid date
        }
    }
}
