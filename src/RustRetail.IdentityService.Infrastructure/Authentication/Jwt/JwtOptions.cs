namespace RustRetail.IdentityService.Infrastructure.Authentication.Jwt
{
    internal class JwtOptions
    {
        public string Issuer { get; init; } = string.Empty!;
        public string Audience { get; init; } = string.Empty!;
        public string SecretKey { get; init; } = string.Empty!;
        public int AccessTokenExpiryInMilliseconds { get; init; }
        public int RefreshTokenExpiryInMilliseconds { get; init; }
    }
}
