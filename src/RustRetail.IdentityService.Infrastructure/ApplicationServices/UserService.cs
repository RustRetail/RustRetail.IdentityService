using Microsoft.Extensions.Options;
using RustRetail.IdentityService.Application.Abstractions.Authentication;
using RustRetail.IdentityService.Application.Abstractions.Services;
using RustRetail.IdentityService.Application.Configuration.Authentication;
using RustRetail.IdentityService.Domain.Entities;
using RustRetail.IdentityService.Domain.Repositories;

namespace RustRetail.IdentityService.Infrastructure.ApplicationServices
{
    internal class UserService(
        IIdentityUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IOptions<AuthenticationSettingOptions> options)
        : IUserService
    {
        readonly IUserRepository _userRepository = unitOfWork.GetRepository<IUserRepository>();
        readonly AuthenticationSettingOptions _authenticationSettings = options.Value;

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _userRepository.GetUserByEmailAsync(email, cancellationToken);
        }

        public async Task<User?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            return await _userRepository.GetUserByUserNameAsync(userName, cancellationToken);
        }

        public async Task IncreaseFailedLoginAttemptsAsync(User user, CancellationToken cancellationToken = default)
        {
            user.AccessFailedCount++;
            if (user.AccessFailedCount >= _authenticationSettings.MaxFailedLoginAttempts)
            {
                await LockoutUserAsync(
                    user,
                    DateTimeOffset.UtcNow.Add(TimeSpan.FromMicroseconds(_authenticationSettings.LockoutDurationInMilliseconds)),
                    cancellationToken);
            }
            _userRepository.Update(user);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public bool IsUserLockedOut(User user, DateTimeOffset currentDateTime)
        {
            if (!user.LockoutEnabled) return false;
            if (user.LockoutEnd is null) return false;
            return user.LockoutEnd > currentDateTime;
        }

        public async Task LockoutUserAsync(User user,
            DateTimeOffset lockoutEndTimestamp,
            CancellationToken cancellationToken = default)
        {
            user.LockoutEnd = lockoutEndTimestamp;
            _userRepository.Update(user);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task ResetFailedLoginAttemptsAsync(User user, CancellationToken cancellationToken = default)
        {
            user.AccessFailedCount = 0;
            user.LockoutEnd = null;
            _userRepository.Update(user);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public bool ValidatePassword(User user, string password)
        {
            return passwordHasher.VerifyPassword(password, user.PasswordHash);
        }
    }
}
