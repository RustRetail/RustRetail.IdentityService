using Microsoft.Extensions.Logging;
using RustRetail.IdentityService.Application.Abstractions.MessageBrokers;
using RustRetail.IdentityService.Domain.Events.User;
using RustRetail.SharedApplication.Abstractions;
using RustRetail.SharedContracts.IntegrationEvents.V1.IdentityService.Authentication;

namespace RustRetail.IdentityService.Application.Authentication.Login
{
    internal class UserLockedOutDomainEventHandler(
        ILogger<UserLockedOutDomainEventHandler> logger,
        IMessageSender messageSender) : IDomainEventHandler<DomainEventNotification<UserLockedOutDomainEvent>>
    {
        public Task Handle(
            DomainEventNotification<UserLockedOutDomainEvent> notification,
            CancellationToken cancellationToken)
        {
            var @event = notification.DomainEvent;
            logger.LogInformation("Handling UserLockedOut domain event: UserId={@UserId}",
                @event.UserId);
            messageSender.PublishAsync(
                new UserLockedOutEvent(@event.UserId) 
                {
                    LockoutDurationInMilliseconds = 900000
                },
                cancellationToken);
            return Task.CompletedTask;
        }
    }
}
