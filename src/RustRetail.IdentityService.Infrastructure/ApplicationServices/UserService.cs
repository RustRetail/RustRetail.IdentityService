using RustRetail.IdentityService.Application.Abstractions.Authentication;
using RustRetail.IdentityService.Application.Abstractions.Services;
using RustRetail.IdentityService.Domain.Entities;
using RustRetail.IdentityService.Domain.Repositories;

namespace RustRetail.IdentityService.Infrastructure.ApplicationServices
{
    internal class UserService(
        IIdentityUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
        : IUserService
    {
        readonly IUserRepository userRepository = unitOfWork.GetRepository<IUserRepository>();

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await userRepository.GetUserByEmailAsync(email, cancellationToken);
        }

        public async Task<User?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            return await userRepository.GetUserByUserNameAsync(userName, cancellationToken);
        }

        public Task<bool> ValidatePasswordAsync(User user, string password)
        {
            return Task.FromResult(passwordHasher.VerifyPassword(password, user.PasswordHash));
        }
    }
}
