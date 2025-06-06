using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.Domain.Errors.Authentication.Login
{
    public static class LoginErrors
    {
        public static readonly Error InvalidCredentials = Error.Unauthorized("Authentication.Login.InvalidCredentials", "Invalid email or password.");
        public static readonly Error UserLockedOut = Error.Unauthorized("Authentication.Login.UserLockedOut", "User is locked out due to too many failed login attempts. Please try again later.");
    }
}
