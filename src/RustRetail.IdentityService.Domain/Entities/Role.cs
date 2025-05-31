using RustRetail.SharedKernel.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace RustRetail.IdentityService.Domain.Entities
{
    public sealed class Role : AggregateRoot<Guid>
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(256)]
        public string NormalizedName { get; set; } = string.Empty;
        [MaxLength(1024)]
        public string? Description { get; set; }

        public ICollection<Permission> Permissions { get; set; } = [];
        public ICollection<UserRole> Users { get; set; } = [];
    }
}
