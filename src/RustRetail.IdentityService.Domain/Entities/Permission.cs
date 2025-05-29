using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.IdentityService.Domain.Entities
{
    public sealed class Permission : IHasKey<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; } = DateTimeOffset.UtcNow;
    }
}
