using RustRetail.SharedKernel.Domain.Events.Domain;

namespace RustRetail.IdentityService.Domain.Events.User
{
    public sealed class UserLockedOutDomainEvent : DomainEvent
    {
        public Guid UserId { get; init; }
        public string Reason { get; init; } = string.Empty;
        public long LockoutDurationInMilliseconds { get; init; }

        public UserLockedOutDomainEvent(Guid userId, string reason = "", long durationInMilliseconds = 900000)
        {
            UserId = userId;
            Reason = reason;
            LockoutDurationInMilliseconds = durationInMilliseconds;
        }
    }
}
