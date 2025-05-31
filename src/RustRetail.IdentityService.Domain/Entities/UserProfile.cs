using RustRetail.IdentityService.Domain.Enums;
using RustRetail.SharedKernel.Domain.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace RustRetail.IdentityService.Domain.Entities
{
    public class UserProfile : IHasKey<Guid>
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set; }

        public Gender? Gender { get; set; }

        [MaxLength(1024)]
        public string? AvatarUrl { get; set; }

        [MaxLength(1024)]
        public string? Bio { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public User User { get; set; } = default!;
    }
}
