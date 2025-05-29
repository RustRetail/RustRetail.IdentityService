using RustRetail.SharedKernel.Domain.Models;

namespace RustRetail.IdentityService.Domain.Entities
{
    public sealed class User : AggregateRoot<Guid>
    {
        public string UserName { get; set; } = string.Empty;
        public string NormalizedUserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NormalizedEmail { get; set; } = string.Empty;
        public bool EmailConfirmed { get; set; } = false;
        public string PasswordHash { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; } = false;
        public bool TwoFactorEnabled { get; set; } = false;
        public bool LockoutEnabled { get; set; } = true;
        public DateTimeOffset? LockoutEnd { get; set; }
        public int AccessFailedCount { get; set; } = 0;

        public ICollection<UserClaim> Claims { get; set; } = [];
        public ICollection<UserToken> Tokens { get; set; } = [];
        public ICollection<UserLogin> Logins { get; set; } = [];
        public ICollection<UserRole> Roles { get; set; } = [];
    }
}
