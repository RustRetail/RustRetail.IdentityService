using RustRetail.SharedKernel.Domain.Events.Domain;

namespace RustRetail.IdentityService.Domain.Events.User
{
    public sealed class UserLockedOutDomainEvent : DomainEvent
    {
        public Guid UserId { get; init; }

        public UserLockedOutDomainEvent(Guid userId)
        {
            UserId = userId;
        }
    }
}
