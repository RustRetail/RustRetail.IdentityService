using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.Domain.Errors.User.Profile
{
    public static class UserProfileErrors
    {
        public static readonly Error UserProfileNotCreated = Error.NotFound("User.Profile.NotCreated", "The user profile has not been created yet.");
    }
}
