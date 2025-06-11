using Microsoft.Extensions.Options;
using RustRetail.IdentityService.Application.Abstractions.Authentication;
using RustRetail.IdentityService.Application.Configuration.Authentication;
using RustRetail.IdentityService.Contracts.Authentication.Login;
using RustRetail.IdentityService.Domain.Constants;
using RustRetail.IdentityService.Domain.Entities;
using RustRetail.IdentityService.Domain.Errors.Authentication.Login;
using RustRetail.IdentityService.Domain.Repositories;
using RustRetail.SharedApplication.Abstractions;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.Application.Authentication.Login
{
    internal class LoginCommandHandler(
        IJwtTokenProvider tokenProvider,
        IPasswordHasher passwordHasher,
        IIdentityUnitOfWork unitOfWork,
        IOptions<AuthenticationSettingOptions> options)
        : ICommandHandler<LoginCommand, LoginResponse>
    {
        readonly IUserRepository userRepository = unitOfWork.GetRepository<IUserRepository>();
        readonly AuthenticationSettingOptions authenticationSettings = options.Value;

        public async Task<Result<LoginResponse>> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            // Get the user by email
            var user = await userRepository.GetUserByEmailAsync(
                request.Email,
                true,
                true,
                cancellationToken,
                u => u.Tokens);

            if (user is null)
            {
                return Result.Failure<LoginResponse>(LoginErrors.InvalidCredentials);
            }

            // Check if the user is locked out
            if (user.IsUserLockedOut(DateTimeOffset.UtcNow))
            {
                return Result.Failure<LoginResponse>(LoginErrors.UserLockedOut);
            }

            // Validate the password
            if (!passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                if (user.LockoutEnabled)
                {
                    user.IncreaseAccessFailedCount();
                    if (user.AccessFailedCount >= authenticationSettings.MaxFailedLoginAttempts)
                    {
                        user.SetLockoutEnd(DateTimeOffset.UtcNow.AddMilliseconds(authenticationSettings.LockoutDurationInMilliseconds));
                    }
                    userRepository.Update(user);
                    await unitOfWork.SaveChangeAsync(cancellationToken);
                }
                return Result.Failure<LoginResponse>(LoginErrors.InvalidCredentials);
            }

            // Generate tokens
            string accessToken = tokenProvider.GenerateAccessToken(user, new List<string>());
            string refreshToken = tokenProvider.GenerateRefreshToken();

            // Reset access failed count
            user.ResetAccessFailedCount();

            // Add or update user's refresh token
            var existingToken = user.Tokens.FirstOrDefault(t =>
                t.Name == UserTokenConstants.RefreshTokenName &&
                t.Provider == UserTokenConstants.RustRetailIdentityServiceProvider);

            if (existingToken == null)
            {
                existingToken = new UserToken()
                {
                    Provider = UserTokenConstants.RustRetailIdentityServiceProvider,
                    Name = UserTokenConstants.RefreshTokenName,
                    Value = refreshToken,
                    CreatedDateTime = DateTimeOffset.UtcNow,
                    ExpiryDateTime = null
                };
                user.Tokens.Add(existingToken);
            }
            else
            {
                existingToken.Value = refreshToken;
                existingToken.CreatedDateTime = DateTimeOffset.UtcNow;
            }

            // Update user
            userRepository.Update(user);
            await unitOfWork.SaveChangeAsync(cancellationToken);

            return Result.Success(
                new LoginResponse(
                    accessToken,
                    refreshToken));
        }
    }
}
