using Microsoft.EntityFrameworkCore;
using RustRetail.IdentityService.Domain.Entities;
using RustRetail.IdentityService.Domain.Repositories;
using RustRetail.IdentityService.Persistence.Database;
using RustRetail.SharedPersistence.Database;

namespace RustRetail.IdentityService.Persistence.Repositories
{
    internal class RoleRepository : Repository<Role, Guid>, IRoleRepository
    {
        public RoleRepository(IdentityDbContext context) : base(context)
        {
        }

        public async Task<Role?> GetRoleByNameAsync(
            string name,
            bool asTracking = false,
            CancellationToken cancellationToken = default)
        {
            QueryTrackingBehavior trackingBehavior = asTracking
                ? QueryTrackingBehavior.TrackAll
                : QueryTrackingBehavior.NoTracking;

            return await _dbSet
                .AsTracking(trackingBehavior)
                .FirstOrDefaultAsync(
                    r => r.NormalizedName == name.Trim().ToUpperInvariant(),
                    cancellationToken);
        }

        public async Task<IReadOnlyList<Role>> GetRolesByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<UserRole>()
                .Where(ur => ur.UserId == userId)
                .Include(ur => ur.Role)
                .Select(ur => ur.Role)
                .ToListAsync(cancellationToken);
        }

        public async Task<Role?> GetRoleWithPermissionByIdAsync(
            Guid roleId,
            CancellationToken cancellationToken = default,
            bool asTracking = true)
        {
            QueryTrackingBehavior trackingBehavior = asTracking
                ? QueryTrackingBehavior.TrackAll
                : QueryTrackingBehavior.NoTracking;

            return await _dbSet
                .AsTracking(trackingBehavior)
                .Include(r => r.Permissions)
                .FirstOrDefaultAsync(
                r => r.Id == roleId,
                cancellationToken);
        }
    }
}
