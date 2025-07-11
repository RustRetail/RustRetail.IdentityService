using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.Domain.Errors.User
{
    public static class UserErrors
    {
        public static readonly Error UserNotFoundWithEmail = Error.NotFound("User.NotFoundWithEmail", "The user was not found with given email.");
        public static readonly Error UserNotFoundWithUserName = Error.NotFound("User.NotFoundWithUserName", "The user was not found with given user name.");
        public static readonly Error UserNotFoundWithId = Error.NotFound("User.NotFoundWithId", "The user was not found with given id.");
    }
}
