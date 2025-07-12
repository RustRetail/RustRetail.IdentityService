using RustRetail.SharedApplication.Abstractions;

namespace RustRetail.IdentityService.Application.Users.CreateOrUpdateProfile
{
    public record CreateOrUpdateUserProfileCommand(
        string? FirstName,
        string? LastName,
        string? Gender,
        string? Bio,
        DateTime? DateOfBirth) : ICommand;
}
