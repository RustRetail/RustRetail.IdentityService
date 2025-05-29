namespace RustRetail.IdentityService.Domain.Entities
{
    public sealed class UserRole
    {
        public DateTimeOffset AssignedDateTime { get; set; } = DateTimeOffset.UtcNow;

        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        public Guid RoleId { get; set; }
        public Role Role { get; set; } = default!;
    }
}
