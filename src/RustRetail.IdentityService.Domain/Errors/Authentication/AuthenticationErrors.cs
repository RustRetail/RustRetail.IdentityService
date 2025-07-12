using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.Domain.Errors.Authentication
{
    public static class AuthenticationErrors
    {
        public static readonly Error RefreshTokenCookieNotFound = Error.Unauthorized(
            "Authentication.Token.RefreshTokenCookieNotFound",
            "Refresh token is missing from the request cookies.");

        public static readonly Error UserNotAuthenticated = Error.Unauthorized(
            "Authentication.User.NotAuthenticated",
            "User is not authenticated.");
    }
}
