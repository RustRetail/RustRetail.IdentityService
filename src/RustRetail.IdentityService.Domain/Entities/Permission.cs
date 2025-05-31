using RustRetail.SharedKernel.Domain.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace RustRetail.IdentityService.Domain.Entities
{
    public sealed class Permission : IHasKey<Guid>
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(1024)]
        public string? Description { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; } = DateTimeOffset.UtcNow;

        public Guid RoleId { get; set; }
        public Role Role { get; set; } = default!;
    }
}
