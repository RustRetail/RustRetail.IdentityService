using RustRetail.SharedKernel.Domain.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace RustRetail.IdentityService.Domain.Entities
{
    public sealed class UserToken : IHasKey<Guid>
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Provider { get; set; } = string.Empty;
        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(1024)]
        public string Value { get; set; } = string.Empty;
        public DateTimeOffset? CreatedDateTime { get; set; }
        public DateTimeOffset? ExpiryDateTime { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = default!;

        public UserToken()
        {
        }
    }
}
