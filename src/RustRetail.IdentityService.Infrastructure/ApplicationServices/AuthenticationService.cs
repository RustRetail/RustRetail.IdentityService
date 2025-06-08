using RustRetail.IdentityService.Application.Abstractions.Authentication;
using RustRetail.IdentityService.Application.Abstractions.Services;
using RustRetail.IdentityService.Contracts.Authentication.Login;
using RustRetail.IdentityService.Domain.Errors.Authentication.Login;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.Infrastructure.ApplicationServices
{
    internal class AuthenticationService(
        IUserService userService,
        IJwtTokenProvider tokenProvider)
        : IAuthenticationService
    {
        public async Task<Result<LoginResponse>> LoginAsync(
            string email,
            string password,
            CancellationToken cancellationToken = default)
        {
            // Get the user by email
            var user = await userService.GetUserByEmailAsync(email, cancellationToken);
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
            if (!userService.ValidatePassword(user, password))
            {
                // To do: Raise login failed domain event
                return Result.Failure<LoginResponse>(LoginErrors.InvalidCredentials);
            }

            string accessToken = tokenProvider.GenerateAccessToken(user, new List<string>());
            string refreshToken = tokenProvider.GenerateRefreshToken();

            // To do: Save the refresh token to the user 
            // To do: Raise user logged in success domain event
            
            return Result.Success(
                new LoginResponse(
                    accessToken,
                    refreshToken));
        }
    }
}
