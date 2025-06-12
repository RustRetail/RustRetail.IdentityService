using RustRetail.IdentityService.Domain.Repositories;
using RustRetail.SharedApplication.Abstractions;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.Application.Authentication.RevokeRefreshToken
{
    internal class RevokeRefreshTokenCommandHandler(
        IIdentityUnitOfWork unitOfWork)
        : ICommandHandler<RevokeRefreshTokenCommand>
    {
        public Task<Result> Handle(
            RevokeRefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
