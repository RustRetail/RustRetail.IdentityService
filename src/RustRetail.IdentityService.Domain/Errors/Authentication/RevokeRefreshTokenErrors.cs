using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.Domain.Errors.Authentication
{
    public static class RevokeRefreshTokenErrors
    {
        public static readonly Error RefreshTokenExpired = Error.Unauthorized("Authentication.RefreshToken.Expired", "The refresh token has expired and cannot be revoked.");
        public static readonly Error MissingRefreshToken = Error.Unauthorized("Authentication.RefreshToken.Missing", "Refresh token is required.");
        public static readonly Error InvalidRefreshToken = Error.Unauthorized("Authentication.RefreshToken.Invalid", "The refresh token is invalid.");
        public static readonly Error RefreshTokenDoesNotBelongToUser = Error.Forbidden("Authentication.RefreshToken.NotBelongToUser", "This token does not belong to the current user.");
        public static readonly Error TokenNotFound = Error.Unauthorized("Authentication.RefreshToken.NotFound", "Cannot find the refresh token.");
    }
}
