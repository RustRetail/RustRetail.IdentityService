using RustRetail.SharedKernel.Domain.Events.Domain;
using RustRetail.SharedKernel.Domain.Events.Integration;

namespace RustRetail.IdentityService.Domain.Events.User
{
    public sealed class UserRegisteredEvent : DomainEvent, IIntegrationEvent
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public UserRegisteredEvent(
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
