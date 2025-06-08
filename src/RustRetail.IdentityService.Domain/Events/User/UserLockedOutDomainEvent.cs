using RustRetail.SharedKernel.Domain.Events.Domain;

namespace RustRetail.IdentityService.Domain.Events.User
{
    public sealed class UserLockedOutDomainEvent : DomainEvent
    {
        Guid UserId { get; }

        public UserLockedOutDomainEvent(Guid userId)
        {
            UserId = userId;
        }
    }
}
