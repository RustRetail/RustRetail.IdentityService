using RustRetail.IdentityService.Contracts.Authentication.Login;
using RustRetail.SharedApplication.Abstractions;

namespace RustRetail.IdentityService.Application.Authentication.Login
{
    public record LoginCommand(
        string Email,
        string Password) : ICommand<LoginResponse>;
}
