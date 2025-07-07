using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.Domain.Errors.User
{
    public static class UserErrors
    {
        public static readonly Error UserNotFoundWithEmail = Error.NotFound("User.NotFoundWithEmail", "The user was not found with given email.");
    }
}
