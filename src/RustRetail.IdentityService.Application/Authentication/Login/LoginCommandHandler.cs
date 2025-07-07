using RustRetail.IdentityService.Application.Abstractions.Authentication;
using RustRetail.IdentityService.Application.Abstractions.Services;
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
        IIdentityUnitOfWork unitOfWork,
        IUserService userService,
        IRoleService roleService)
        : ICommandHandler<LoginCommand, LoginResponse>
    {
        readonly IUserRepository userRepository = unitOfWork.GetRepository<IUserRepository>();

        public async Task<Result<LoginResponse>> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            // Get the user by email
            var user = await userService.GetUserByEmailAsync(
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
            if (userService.IsUserLockedOut(user, DateTimeOffset.UtcNow))
            {
                return Result.Failure<LoginResponse>(LoginErrors.UserLockedOut);
            }

            // Validate the password
            if (!userService.ValidateUserPassword(user, request.Password))
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
            userRepository.Update(user);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(
                new LoginResponse(
                    accessToken,
                    refreshToken));
        }
    }
}
