using RustRetail.IdentityService.Contracts.Authentication.Login;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.Application.Abstractions.Services
{
    public interface IAuthenticationService
    {
        Task<Result<LoginResponse>> LoginAsync(string email, string password, CancellationToken cancellationToken = default);
    }
}
