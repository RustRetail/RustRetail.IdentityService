using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.Domain.Entities
{
    public sealed class UserClaim : IHasKey<Guid>
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public DateTimeOffset CreatedDateTime { get; set; } = DateTimeOffset.UtcNow;

        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
