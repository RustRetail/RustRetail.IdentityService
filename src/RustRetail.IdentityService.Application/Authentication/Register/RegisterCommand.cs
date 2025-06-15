using RustRetail.SharedApplication.Abstractions;

namespace RustRetail.IdentityService.Application.Authentication.Register
{
    public record RegisterCommand(
        string Email,
        string Password,
        string ConfirmPassword,
        string UserName) : ICommand;
}
