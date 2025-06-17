using Microsoft.Extensions.Logging;
using RustRetail.IdentityService.Domain.Events.User;
using RustRetail.SharedApplication.Abstractions;

namespace RustRetail.IdentityService.Application.Authentication.Register
{
    internal class UserRegisteredDomainEventHandler(
        ILogger<UserRegisteredDomainEventHandler> logger) :
        IDomainEventHandler<DomainEventNotification<UserRegisteredEvent>>
    {
        public async Task Handle(
            DomainEventNotification<UserRegisteredEvent> notification,
            CancellationToken cancellationToken)
        {
            var e = notification.DomainEvent;
            logger.LogInformation("Handling UserRegisteredDomainEvent");
            logger.LogInformation("Event Id: {@Id}", e.Id);
            logger.LogInformation("Event OccurredOn: {@OccurredOn}", new DateTimeOffset(e.OccurredOn));
            logger.LogInformation("User register with information:");
            logger.LogInformation("User Id: {@UserId}", e.UserId);
            logger.LogInformation("Username: {@UserName}", e.UserName);
            logger.LogInformation("Email: {@Email}", e.Email);
        }
    }
}
