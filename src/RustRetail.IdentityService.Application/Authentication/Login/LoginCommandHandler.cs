using RustRetail.IdentityService.Application.Abstractions.Services;
using RustRetail.IdentityService.Contracts.Authentication.Login;
using RustRetail.SharedApplication.Abstractions;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.Application.Authentication.Login
{
    internal class LoginCommandHandler(
        IAuthenticationService authenticationService)
        : ICommandHandler<LoginCommand, LoginResponse>
    {
        public async Task<Result<LoginResponse>> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            return await authenticationService.LoginWithEmailAsync(request.Email,
                request.Password,
                cancellationToken);
        }
    }
}
