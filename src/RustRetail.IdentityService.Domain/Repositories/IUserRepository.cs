using RustRetail.IdentityService.Domain.Entities;
using RustRetail.SharedKernel.Domain.Abstractions;
using System.Linq.Expressions;

namespace RustRetail.IdentityService.Domain.Repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<User?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken = default);
        Task<User?> GetUserByEmailAsync(string email, bool asTracking = true, bool asSplitQuery = false, CancellationToken cancellationToken = default, params Expression<Func<User, object>>[] includes);
    }
}
