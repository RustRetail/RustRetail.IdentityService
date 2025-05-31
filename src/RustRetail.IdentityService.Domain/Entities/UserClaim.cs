using RustRetail.SharedKernel.Domain.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace RustRetail.IdentityService.Domain.Entities
{
    public sealed class UserClaim : IHasKey<Guid>
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Type { get; set; } = string.Empty;
        [Required]
        [MaxLength(1024)]
        public string Value { get; set; } = string.Empty;
        public DateTimeOffset CreatedDateTime { get; set; } = DateTimeOffset.UtcNow;

        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
