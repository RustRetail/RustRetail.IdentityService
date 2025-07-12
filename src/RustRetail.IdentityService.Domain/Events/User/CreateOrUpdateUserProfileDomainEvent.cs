using RustRetail.SharedKernel.Domain.Events.Domain;

namespace RustRetail.IdentityService.Domain.Events.User
{
    public class CreateOrUpdateUserProfileDomainEvent : DomainEvent
    {
        public CreateOrUpdateUserProfileDomainEvent(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; init; }
    }
}
