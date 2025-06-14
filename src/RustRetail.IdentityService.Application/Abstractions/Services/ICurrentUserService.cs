namespace RustRetail.IdentityService.Application.Abstractions.Services
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        string? NormalizedEmail { get; }
        string? NormalizedUserName { get; }
        bool IsAuthenticated { get; }
        IEnumerable<string> Roles { get; }
    }
}
