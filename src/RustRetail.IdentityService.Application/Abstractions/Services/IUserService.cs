using RustRetail.IdentityService.Domain.Entities;
using System.Linq.Expressions;

namespace RustRetail.IdentityService.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<User?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken = default);
        bool ValidateUserPassword(User user, string password);
        bool IsUserLockedOut(User user, DateTimeOffset currentDateTime);
        Task IncreaseFailedLoginAttemptsAsync(User user, CancellationToken cancellationToken = default);
        Task<User?> GetUserByEmailAsync(string email, bool asTracking = true, bool asSplitQuery = false, CancellationToken cancellationToken = default, params Expression<Func<User, object>>[] includes);
        void AddOrUpdateUserToken(User user, UserToken userToken);
    }
}
