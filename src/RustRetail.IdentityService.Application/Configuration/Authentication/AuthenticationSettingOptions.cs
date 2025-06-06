namespace RustRetail.IdentityService.Application.Configuration.Authentication
{
    public class AuthenticationSettingOptions
    {
        public const string SectionName = "AuthenticationSettings";

        public int MaxFailedLoginAttempts { get; init; } = 5;
        public int LockoutDurationInMilliseconds { get; init; } = 900000;
    }
}
