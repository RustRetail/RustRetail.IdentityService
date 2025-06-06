using Microsoft.EntityFrameworkCore;
using RustRetail.IdentityService.Domain.Entities;
using RustRetail.IdentityService.Domain.Repositories;
using RustRetail.IdentityService.Persistence.Database;
using RustRetail.SharedPersistence.Database;

namespace RustRetail.IdentityService.Persistence.Repositories
{
    internal class UserRepository : Repository<User, Guid>, IUserRepository
    {
        public UserRepository(IdentityDbContext context) : base(context)
        {
        }

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(
                u => u.NormalizedEmail == email.Trim().ToUpperInvariant(),
                cancellationToken);
        }

        public async Task<User?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(
                u => u.NormalizedUserName == userName.Trim().ToUpperInvariant(),
                cancellationToken);
        }
    }
}
