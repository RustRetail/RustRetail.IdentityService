using RustRetail.IdentityService.Domain.Entities;

namespace RustRetail.IdentityService.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<User?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken = default);
        Task<bool> ValidatePasswordAsync(User user, string password);
    }
}
