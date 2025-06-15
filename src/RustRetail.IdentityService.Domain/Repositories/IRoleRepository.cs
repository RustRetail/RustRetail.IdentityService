using RustRetail.IdentityService.Domain.Entities;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.Domain.Repositories
{
    public interface IRoleRepository : IRepository<Role, Guid>
    {
        Task<Role?> GetRoleWithPermissionByIdAsync(Guid roleId, CancellationToken cancellationToken = default, bool asTracking = true);
        Task<IReadOnlyList<Role>> GetRolesByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<Role?> GetRoleByNameAsync(string name, bool asTracking = false, CancellationToken cancellationToken = default);
    }
}
