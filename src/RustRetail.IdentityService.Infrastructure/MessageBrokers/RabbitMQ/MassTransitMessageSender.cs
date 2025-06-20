using MassTransit;
using RustRetail.IdentityService.Application.Abstractions.MessageBrokers;

namespace RustRetail.IdentityService.Infrastructure.MessageBrokers.RabbitMQ
{
    internal class MassTransitMessageSender : IMessageSender
    {
        readonly IPublishEndpoint _publishEndpoint;

        public MassTransitMessageSender(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class
        {
            await _publishEndpoint.Publish(message, cancellationToken);
        }
    }
}
