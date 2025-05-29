using RustRetail.SharedKernel.Domain.Models;

namespace RustRetail.IdentityService.Domain.Entities
{
    public sealed class Role : AggregateRoot<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string NormalizedName { get; set; } = string.Empty;
        public string? Description { get; set; }

        public ICollection<Permission> Permissions { get; set; } = [];
        public ICollection<UserRole> Users { get; set; } = [];
    }
}
