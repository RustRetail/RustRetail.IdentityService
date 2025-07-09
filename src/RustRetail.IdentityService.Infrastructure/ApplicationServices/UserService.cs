using Microsoft.Extensions.Options;
using RustRetail.IdentityService.Application.Abstractions.Authentication;
using RustRetail.IdentityService.Application.Abstractions.Services;
using RustRetail.IdentityService.Application.Configuration.Authentication;
using RustRetail.IdentityService.Domain.Entities;
using RustRetail.IdentityService.Domain.Repositories;
using System.Linq.Expressions;

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

        public void AddOrUpdateUserToken(User user, UserToken userToken)
        {
            // Add or update user's refresh token
            var existingToken = user.Tokens.FirstOrDefault(t =>
                t.Name == userToken.Name &&
                t.Provider == userToken.Provider);

            if (existingToken is null)
            {
                user.Tokens.Add(userToken);
            }
            else
            {
                existingToken.Value = userToken.Value;
                existingToken.CreatedDateTime = DateTimeOffset.UtcNow;
                existingToken.ExpiryDateTime = userToken.ExpiryDateTime;
            }
        }

        public Task<User?> GetUserByEmailAsync(string email, bool asTracking = true, bool asSplitQuery = false, CancellationToken cancellationToken = default, params Expression<Func<User, object>>[] includes)
        {
            return _userRepository.GetUserByEmailAsync(
                email,
                asTracking,
                asSplitQuery,
                cancellationToken,
                includes);
        }

        public async Task<User?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            return await _userRepository.GetUserByUserNameAsync(userName, cancellationToken);
        }

        public async Task IncreaseFailedLoginAttemptsAsync(User user, CancellationToken cancellationToken = default)
        {
            if (user.LockoutEnabled)
            {
                user.IncreaseAccessFailedCount();
                if (user.AccessFailedCount >= _authenticationSettings.MaxFailedLoginAttempts)
                {
                    user.SetLockoutEnd(DateTimeOffset.UtcNow.AddMicroseconds(_authenticationSettings.LockoutDurationInMilliseconds));
                }
                _userRepository.Update(user);
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }

        public bool IsUserLockedOut(User user, DateTimeOffset currentDateTime)
        {
            return user.IsUserLockedOut(currentDateTime);
        }

        public async Task<int> UpdateUserAsync(User user, CancellationToken cancellationToken = default)
        {
            _userRepository.Update(user);
            return await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public bool ValidateUserPassword(User user, string password)
        {
            return passwordHasher.VerifyPassword(password, user.PasswordHash);
        }
    }
}
