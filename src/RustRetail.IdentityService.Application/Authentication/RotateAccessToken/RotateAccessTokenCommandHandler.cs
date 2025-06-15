using RustRetail.IdentityService.Application.Abstractions.Authentication;
using RustRetail.IdentityService.Contracts.Authentication.RotateAccessToken;
using RustRetail.IdentityService.Domain.Errors.Authentication;
using RustRetail.IdentityService.Domain.Repositories;
using RustRetail.SharedApplication.Abstractions;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.Application.Authentication.RotateAccessToken
{
    internal class RotateAccessTokenCommandHandler(
        IIdentityUnitOfWork unitOfWork,
        IJwtTokenProvider tokenProvider)
        : ICommandHandler<RotateAccessTokenCommand, RotateAccessTokenResponse>
    {
        readonly IUserRepository userRepository = unitOfWork.GetRepository<IUserRepository>();
        readonly IRoleRepository roleRepository = unitOfWork.GetRepository<IRoleRepository>();

        public async Task<Result<RotateAccessTokenResponse>> Handle(
            RotateAccessTokenCommand request,
            CancellationToken cancellationToken)
        {
            // Check if token is provided
            if (string.IsNullOrWhiteSpace(request.refreshToken))
            {
                return Result.Failure<RotateAccessTokenResponse>(RefreshTokenErrors.MissingRefreshToken);
            }

            // Check if the token is valid and not expired
            var expiryDate = tokenProvider.GetExpiryFromToken(request.refreshToken);
            if (expiryDate == null)
            {
                return Result.Failure<RotateAccessTokenResponse>(RefreshTokenErrors.InvalidRefreshToken);
            }
            if (expiryDate <= DateTime.UtcNow)
            {
                return Result.Failure<RotateAccessTokenResponse>(RefreshTokenErrors.RefreshTokenExpired);
            }

            // Get the user by refresh token
            var user = await userRepository.GetUserByRefreshTokenAsync(
                request.refreshToken,
                cancellationToken);
            if (user is null)
            {
                return Result.Failure<RotateAccessTokenResponse>(RefreshTokenErrors.InvalidRefreshToken);
            }

            var refreshToken = user.Tokens.FirstOrDefault(t => t.Value == request.refreshToken);
            if (refreshToken is null)
            {
                return Result.Failure<RotateAccessTokenResponse>(RefreshTokenErrors.TokenNotFound);
            }

            // Generate new tokens
            var roles = await roleRepository.GetRolesByUserIdAsync(user.Id);
            var newAccessToken = tokenProvider.GenerateAccessToken(user, roles.Select(r => r.NormalizedName).ToList());
            var newRefreshToken = tokenProvider.GenerateRefreshToken();

            // Rotate existing refresh token
            refreshToken.Value = newRefreshToken;
            refreshToken.CreatedDateTime = DateTimeOffset.UtcNow;
            refreshToken.ExpiryDateTime = tokenProvider.RefreshTokenExpiry();

            userRepository.Update(user);
            await unitOfWork.SaveChangeAsync(cancellationToken);

            return Result.Success(new RotateAccessTokenResponse(newAccessToken, newRefreshToken));
        }
    }
}
