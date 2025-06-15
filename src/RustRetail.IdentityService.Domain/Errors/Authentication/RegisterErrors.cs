using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.Domain.Errors.Authentication
{
    public static class RegisterErrors
    {
        public static readonly Error EmailExisted = Error.Conflict("Authentication.Register.EmailExisted", "Email already exists.");
        public static readonly Error UserNameExisted = Error.Conflict("Authentication.Register.UserNameExisted", "Username already exists.");
    }
}
