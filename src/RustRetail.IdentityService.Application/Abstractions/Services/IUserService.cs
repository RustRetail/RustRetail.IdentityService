using RustRetail.IdentityService.Domain.Entities;

namespace RustRetail.IdentityService.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<User?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken = default);
        bool ValidatePasswordAsync(User user, string password);
        bool IsLockedOutAsync(User user, DateTimeOffset currentDateTime);
        Task IncreaseFailedLoginAttemptsAsync(User user, CancellationToken cancellationToken = default);
        Task LockoutUserAsync(User user, DateTimeOffset lockoutEndTimestamp, CancellationToken cancellationToken = default);
        Task ResetFailedLoginAttemptsAsync(User user, CancellationToken cancellationToken = default);
    }
}
