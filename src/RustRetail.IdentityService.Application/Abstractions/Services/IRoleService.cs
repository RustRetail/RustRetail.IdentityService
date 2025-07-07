namespace RustRetail.IdentityService.Application.Abstractions.Services
{
    public interface IRoleService
    {
        Task<List<string>> GetRolesByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    }
}
