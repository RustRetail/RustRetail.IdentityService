namespace RustRetail.IdentityService.Contracts.Users.CreateOrUpdateProfile
{
    public record CreateOrUpdateUserProfileRequest(
        string FirstName,
        string LastName,
        string Gender,
        string? Bio,
        string? DateOfBirth);
}
