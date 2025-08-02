namespace RustRetail.IdentityService.Contracts.Users.GetProfile
{
    public record GetUserProfileResponse(
        Guid UserId,
        string? FirstName,
        string? LastName,
        string? Gender,
        string? AvatarUrl,
        string? Bio,
        DateTime? DateOfBirth)
    {
        public static GetUserProfileResponse Empty(Guid userId) =>
            new(userId,
                null,
                null,
                null,
                null,
                null,
                null);
    }
}
