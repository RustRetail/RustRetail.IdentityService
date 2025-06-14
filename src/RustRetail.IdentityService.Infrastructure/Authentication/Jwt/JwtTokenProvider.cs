using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RustRetail.IdentityService.Application.Abstractions.Authentication;
using RustRetail.IdentityService.Domain.Constants;
using RustRetail.IdentityService.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RustRetail.IdentityService.Infrastructure.Authentication.Jwt
{
    internal class JwtTokenProvider(IOptions<JwtOptions> options) : IJwtTokenProvider
    {
        readonly JwtOptions _jwtOptions = options.Value ?? throw new ArgumentNullException(nameof(options), "JWT options cannot be null.");

        public string GenerateAccessToken(User user, IList<string> roles)
        {
            ArgumentNullException.ThrowIfNull(user);
            ArgumentNullException.ThrowIfNull(roles);
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
                new Claim(JwtClaimTypes.Email, user.NormalizedEmail),
                new Claim(JwtClaimTypes.UserName, user.NormalizedUserName),
                new Claim(JwtClaimTypes.Jti, Guid.NewGuid().ToString())
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(JwtClaimTypes.Roles, role));
            }
            var creds = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMilliseconds(_jwtOptions.AccessTokenExpiryInMilliseconds),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtClaimTypes.Jti, Guid.NewGuid().ToString())
            };
            var creds = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMilliseconds(_jwtOptions.RefreshTokenExpiryInMilliseconds),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public DateTime? GetExpiryFromToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            var handler = new JwtSecurityTokenHandler();

            if (!handler.CanReadToken(token))
            {
                return null;
            }

            var jwtToken = handler.ReadJwtToken(token);

            // Datetime in UTC
            return jwtToken.ValidTo;
        }
    }
}
