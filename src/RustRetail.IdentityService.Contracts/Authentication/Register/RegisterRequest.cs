namespace RustRetail.IdentityService.Contracts.Authentication.Register
{
    public record RegisterRequest(
        string Email,
        string Password,
        string ConfirmPassword,
        string UserName);
}
