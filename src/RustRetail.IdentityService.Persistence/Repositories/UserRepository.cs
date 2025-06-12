using Microsoft.EntityFrameworkCore;
using RustRetail.IdentityService.Domain.Constants;
using RustRetail.IdentityService.Domain.Entities;
using RustRetail.IdentityService.Domain.Repositories;
using RustRetail.IdentityService.Persistence.Database;
using RustRetail.SharedPersistence.Database;
using System.Linq.Expressions;

namespace RustRetail.IdentityService.Persistence.Repositories
{
    internal class UserRepository : Repository<User, Guid>, IUserRepository
    {
        public UserRepository(IdentityDbContext context) : base(context)
        {
        }

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            string formattedEmail = email.Trim().ToUpperInvariant();
            return await _dbSet
                .FirstOrDefaultAsync(
                u => u.NormalizedEmail == formattedEmail,
                cancellationToken);
        }

        public Task<User?> GetUserByEmailAsync(
            string email,
            bool asTracking = true,
            bool asSplitQuery = false,
            CancellationToken cancellationToken = default,
            params Expression<Func<User, object>>[] includes)
        {
            string formattedEmail = email.Trim().ToUpperInvariant();

            var query = _dbSet.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            query = asTracking ? query.AsTracking() : query.AsNoTracking();

            query = asSplitQuery ? query.AsSplitQuery() : query;

            return query.FirstOrDefaultAsync(
                u => u.NormalizedEmail == formattedEmail,
                cancellationToken);
        }

        public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsTracking()
                .Include(u => u.Tokens)
                .FirstOrDefaultAsync(
                u => u.Tokens.Any(t => t.Name == UserTokenConstants.RefreshTokenName && t.Provider == UserTokenConstants.RustRetailIdentityServiceProvider && t.Value == refreshToken),
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
