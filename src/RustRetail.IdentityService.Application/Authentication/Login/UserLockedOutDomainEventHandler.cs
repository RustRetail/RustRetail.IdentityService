﻿using Newtonsoft.Json;
using RustRetail.IdentityService.Domain.Events.User;
using RustRetail.SharedApplication.Abstractions;
using RustRetail.SharedApplication.Behaviors.Outbox;
using RustRetail.SharedContracts.IntegrationEvents.V1.IdentityService.Authentication;
using RustRetail.SharedKernel.Domain.Models;

namespace RustRetail.IdentityService.Application.Authentication.Login
{
    internal class UserLockedOutDomainEventHandler(
        IOutboxMessageService outboxMessageService)
        : IDomainEventHandler<DomainEventNotification<UserLockedOutDomainEvent>>
    {
        public async Task Handle(
            DomainEventNotification<UserLockedOutDomainEvent> notification,
            CancellationToken cancellationToken)
        {
            var @event = notification.DomainEvent;

            var integrationEvent = new UserLockedOutEvent(@event.UserId)
            {
                Reason = @event.Reason,
                LockoutDurationInMilliseconds = (int)@event.LockoutDurationInMilliseconds,
            };
            await outboxMessageService.AddOutboxMessageAsync(
                new OutboxMessage(
                    integrationEvent.GetType().AssemblyQualifiedName!,
                    JsonConvert.SerializeObject(integrationEvent, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All })),
                cancellationToken);
        }
    }
}
