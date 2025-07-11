namespace RustRetail.IdentityService.Contracts.Users.GetProfileById
{
    public record GetUserProfileByIdResponse(
        Guid UserId,
        string? FirstName,
        string? LastName,
        string? Gender,
        string? AvatarUrl,
        string? Bio,
        DateTime? DateOfBirth);
}
