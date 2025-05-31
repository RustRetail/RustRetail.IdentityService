using RustRetail.SharedKernel.Domain.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace RustRetail.IdentityService.Domain.Entities
{
    public sealed class UserLogin : IHasKey<Guid>
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string LoginProvider { get; set; } = string.Empty;
        [Required]
        [MaxLength(1024)]
        public string ProviderKey { get; set; } = string.Empty;
        [MaxLength(256)]
        public string? ProviderDisplayName { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; } = DateTimeOffset.UtcNow;

        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
