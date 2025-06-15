namespace RustRetail.IdentityService.Contracts.Authentication.RotateAccessToken
{
    public record RotateAccessTokenResponse(
        string AccessToken,
        string RefreshToken);
}
