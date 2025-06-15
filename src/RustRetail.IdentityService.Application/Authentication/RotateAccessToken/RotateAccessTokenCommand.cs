using RustRetail.IdentityService.Contracts.Authentication.RotateAccessToken;
using RustRetail.SharedApplication.Abstractions;

namespace RustRetail.IdentityService.Application.Authentication.RotateAccessToken
{
    public record RotateAccessTokenCommand(
        string refreshToken) : ICommand<RotateAccessTokenResponse>;
}
