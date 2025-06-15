using RustRetail.IdentityService.Application.Abstractions.Authentication;
using RustRetail.IdentityService.Application.Abstractions.Services;
using RustRetail.IdentityService.Domain.Errors.Authentication;
using RustRetail.IdentityService.Domain.Repositories;
using RustRetail.SharedApplication.Abstractions;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.Application.Authentication.RevokeRefreshToken
{
    internal class RevokeRefreshTokenCommandHandler(
        IIdentityUnitOfWork unitOfWork,
        ICurrentUserService currentUserService,
        IJwtTokenProvider tokenProvider)
        : ICommandHandler<RevokeRefreshTokenCommand>
    {
        readonly IUserRepository userRepository = unitOfWork.GetRepository<IUserRepository>();

        public async Task<Result> Handle(
            RevokeRefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.refreshToken))
            {
                return Result.Failure(RefreshTokenErrors.MissingRefreshToken);
            }

            var expiryDate = tokenProvider.GetExpiryFromToken(request.refreshToken);
            if (expiryDate == null)
            {
                return Result.Failure(RefreshTokenErrors.InvalidRefreshToken);
            }
            if (expiryDate <= DateTime.UtcNow)
            {
                return Result.Failure(RefreshTokenErrors.RefreshTokenExpired);
            }

            var user = await userRepository.GetUserByRefreshTokenAsync(
                request.refreshToken,
                cancellationToken);

            if (user is null)
            {
                return Result.Failure(RefreshTokenErrors.InvalidRefreshToken);
            }

            if (currentUserService.UserId != user.Id.ToString())    
            {
                return Result.Failure(RefreshTokenErrors.RefreshTokenDoesNotBelongToUser);
            }

            var refreshToken = user.Tokens.FirstOrDefault(t => t.Value == request.refreshToken);
            if (refreshToken is null)
            {
                return Result.Failure(RefreshTokenErrors.TokenNotFound);
            }
            user.Tokens.Remove(refreshToken);
            await unitOfWork.SaveChangeAsync(cancellationToken);

            return Result.Success();
        }
    }
}
