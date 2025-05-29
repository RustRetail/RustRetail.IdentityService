using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.Domain.Entities
{
    public sealed class UserToken : IHasKey<Guid>
    {
        public Guid Id { get; set; }
        public string Provider { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public DateTimeOffset? CreatedDateTime { get; set; }
        public DateTimeOffset? ExpiryDateTime { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
