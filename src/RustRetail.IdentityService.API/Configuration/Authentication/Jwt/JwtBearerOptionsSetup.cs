using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RustRetail.IdentityService.Infrastructure.Authentication.Jwt;
using System.Text;

namespace RustRetail.IdentityService.API.Configuration.Authentication.Jwt
{
    public class JwtBearerOptionsSetup(IOptions<JwtOptions> options) : IConfigureNamedOptions<JwtBearerOptions>
    {
        readonly JwtOptions _jwtOptions = options.Value ?? throw new ArgumentNullException(nameof(options), "JWT options cannot be null.");

        public void Configure(string? name, JwtBearerOptions options)
        {
            // Disable the default claim mapping to prevent issues
            options.MapInboundClaims = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtOptions.Issuer,
                ValidAudience = _jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                ClockSkew = TimeSpan.Zero,
                RoleClaimType = "roles",
            };
        }

        public void Configure(JwtBearerOptions options)
        {
            // Disable the default claim mapping to prevent issues
            options.MapInboundClaims = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtOptions.Issuer,
                ValidAudience = _jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                ClockSkew = TimeSpan.Zero,
            };
        }
    }
}
