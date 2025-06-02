using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RustRetail.IdentityService.Application.Abstractions.Authentication;
using RustRetail.IdentityService.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RustRetail.IdentityService.Infrastructure.Authentication.Jwt
{
    internal class JwtTokenProvider(IOptions<JwtOptions> options) : IJwtTokenProvider
    {
        readonly JwtOptions _jwtOptions = options.Value ?? throw new ArgumentNullException(nameof(options), "JWT options cannot be null.");

        const string UserNameClaim = "userName";
        const string RolesClaim = "roles";


        public string GenerateAccessToken(User user, IList<string> roles)
        {
            if (user is null || roles is null)
            {
                throw new ArgumentNullException("User or user roles cannot be null.");
            }
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.NormalizedEmail),
                new Claim(UserNameClaim, user.NormalizedUserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(RolesClaim, role));
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
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
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
    }
}
