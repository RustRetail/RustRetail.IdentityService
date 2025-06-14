using Microsoft.AspNetCore.Http;
using RustRetail.IdentityService.Application.Abstractions.Services;
using RustRetail.IdentityService.Domain.Constants;

namespace RustRetail.IdentityService.Infrastructure.ApplicationServices
{
    internal class CurrentUserService : ICurrentUserService
    {
        readonly IHttpContextAccessor _contextAccessor;

        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            ArgumentNullException.ThrowIfNull(contextAccessor);
            _contextAccessor = contextAccessor;
        }

        public string? UserId
            => _contextAccessor.HttpContext?.User?.FindFirst(JwtClaimTypes.Subject)?.Value;

        public string? NormalizedEmail 
            => _contextAccessor.HttpContext?.User?.FindFirst(JwtClaimTypes.Email)?.Value;

        public string? NormalizedUserName
            => _contextAccessor.HttpContext?.User?.FindFirst(JwtClaimTypes.UserName)?.Value;

        public bool IsAuthenticated
            => _contextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == true;

        public IEnumerable<string> Roles
            => _contextAccessor.HttpContext?.User?.FindAll(JwtClaimTypes.Roles)?.Select(c => c.Value) ?? Enumerable.Empty<string>();
    }
}
