using Newtonsoft.Json;
using RustRetail.IdentityService.Domain.Events.User;
using RustRetail.SharedApplication.Abstractions;
using RustRetail.SharedApplication.Behaviors.Outbox;
using RustRetail.SharedContracts.IntegrationEvents.V1.IdentityService.User;
using RustRetail.SharedKernel.Domain.Models;

namespace RustRetail.IdentityService.Application.Users.CreateOrUpdateProfile
{
    internal class CreateOrUpdateUserProfileDomainEventHandler(
        IOutboxMessageService outboxMessageService)
        : IDomainEventHandler<DomainEventNotification<CreateOrUpdateUserProfileDomainEvent>>
    {
        public async Task Handle(DomainEventNotification<CreateOrUpdateUserProfileDomainEvent> notification, CancellationToken cancellationToken)
        {
            var @event = notification.DomainEvent;

            var integrationEvent = new UserUpdatedProfileEvent(@event.UserId, @event.OccurredOn);
            await outboxMessageService.AddOutboxMessageAsync(
                new OutboxMessage(
                    integrationEvent.GetType().AssemblyQualifiedName!,
                    JsonConvert.SerializeObject(integrationEvent, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All })),
                cancellationToken);
        }
    }
}
