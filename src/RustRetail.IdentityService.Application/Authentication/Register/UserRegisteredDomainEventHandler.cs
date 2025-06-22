using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RustRetail.IdentityService.Domain.Events.User;
using RustRetail.SharedApplication.Abstractions;
using RustRetail.SharedApplication.Behaviors.Outbox;
using RustRetail.SharedContracts.IntegrationEvents.V1.IdentityService.User;
using RustRetail.SharedKernel.Domain.Models;

namespace RustRetail.IdentityService.Application.Authentication.Register
{
    internal class UserRegisteredDomainEventHandler(
        ILogger<UserRegisteredDomainEventHandler> logger,
        IOutboxMessageService outboxMessageService) :
        IDomainEventHandler<DomainEventNotification<UserRegisteredDomainEvent>>
    {
        public async Task Handle(
            DomainEventNotification<UserRegisteredDomainEvent> notification,
            CancellationToken cancellationToken)
        {
            var @event = notification.DomainEvent;

            var integrationEvent = new UserRegisteredEvent(@event.UserId, @event.UserName, @event.Email, @event.OccurredOn);
            await outboxMessageService.AddOutboxMessageAsync(
                new OutboxMessage(
                    integrationEvent.GetType().AssemblyQualifiedName!,
                    JsonConvert.SerializeObject(integrationEvent, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All})),
                cancellationToken);
        }
    }
}
