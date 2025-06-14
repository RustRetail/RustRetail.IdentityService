using RustRetail.IdentityService.Domain.Entities;

namespace RustRetail.IdentityService.Application.Abstractions.Authentication
{
    public interface IJwtTokenProvider
    {
        string GenerateAccessToken(User user, IList<string> roles);
        string GenerateRefreshToken();
        DateTime? GetExpiryFromToken(string token);
    }
}
