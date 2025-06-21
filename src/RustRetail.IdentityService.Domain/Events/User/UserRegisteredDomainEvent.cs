using RustRetail.SharedKernel.Domain.Events.Domain;

namespace RustRetail.IdentityService.Domain.Events.User
{
    public sealed class UserRegisteredDomainEvent : DomainEvent
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public UserRegisteredDomainEvent(
            Guid userId,
            string username,
            string email)
        {
            UserId = userId;
            UserName = username;
            Email = email;
        }
    }
}
