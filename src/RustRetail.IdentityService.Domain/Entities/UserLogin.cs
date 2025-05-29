using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.Domain.Entities
{
    public sealed class UserLogin : IHasKey<Guid>
    {
        public Guid Id { get; set; }
        public string LoginProvider { get; set; } = string.Empty;
        public string ProviderKey { get; set; } = string.Empty;
        public string? ProviderDisplayName { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; } = DateTimeOffset.UtcNow;

        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
