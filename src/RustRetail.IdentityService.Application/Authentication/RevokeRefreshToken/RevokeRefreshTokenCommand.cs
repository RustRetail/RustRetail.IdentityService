using RustRetail.SharedApplication.Abstractions;

namespace RustRetail.IdentityService.Application.Authentication.RevokeRefreshToken
{
    public record RevokeRefreshTokenCommand(
        string refreshToken) : ICommand;
}
