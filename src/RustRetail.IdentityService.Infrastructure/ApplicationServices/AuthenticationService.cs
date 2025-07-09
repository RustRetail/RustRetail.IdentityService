using RustRetail.IdentityService.Application.Abstractions.Authentication;
using RustRetail.IdentityService.Application.Abstractions.Services;
using RustRetail.IdentityService.Contracts.Authentication.Login;
using RustRetail.IdentityService.Domain.Constants;
using RustRetail.IdentityService.Domain.Entities;
using RustRetail.IdentityService.Domain.Errors.Authentication.Login;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.Infrastructure.ApplicationServices
{
    internal class AuthenticationService(
        IUserService userService,
        IJwtTokenProvider tokenProvider,
        IRoleService roleService)
        : IAuthenticationService
    {
        public async Task<Result<LoginResponse>> LoginWithEmailAsync(
            string email,
            string password,
            CancellationToken cancellationToken = default)
        {
            // Fetch user with tokens included
            var user = await userService.GetUserByEmailAsync(email, asTracking: true, asSplitQuery: true, cancellationToken, u => u.Tokens);
            if (user is null)
            {
                return Result.Failure<LoginResponse>(LoginErrors.InvalidCredentials);
            }

            // Check if the user is locked out
            if (userService.IsUserLockedOut(user, DateTimeOffset.UtcNow))
            {
                return Result.Failure<LoginResponse>(LoginErrors.UserLockedOut);
            }

            // Validate the password
            if (!userService.ValidateUserPassword(user, password))
            {
                await userService.IncreaseFailedLoginAttemptsAsync(user, cancellationToken);
                return Result.Failure<LoginResponse>(LoginErrors.InvalidCredentials);
            }

            // Get user roles
            var userRoles = await roleService.GetRolesByUserIdAsync(user.Id, cancellationToken);

            // Generate tokens
            string accessToken = tokenProvider.GenerateAccessToken(user, userRoles);
            string refreshToken = tokenProvider.GenerateRefreshToken();

            // Reset access failed count
            user.ResetAccessFailedCount();

            // Add or update user's refresh token
            var userRefreshToken = UserToken.Create(UserTokenConstants.RustRetailIdentityServiceProvider,
                UserTokenConstants.RefreshTokenName,
                refreshToken,
                tokenProvider.RefreshTokenExpiry());
            userService.AddOrUpdateUserToken(user, userRefreshToken);

            // Update user
            await userService.UpdateUserAsync(user, cancellationToken);

            return Result.Success(
                new LoginResponse(
                    accessToken,
                    refreshToken));
        }
    }
}
