using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using RustRetail.SharedApplication.Behaviors.Messaging;
using RustRetail.SharedApplication.Behaviors.Outbox;

namespace RustRetail.IdentityService.Infrastructure.BackgroundJobs.Quartz
{
    internal class OutboxPublisherJob : IJob
    {
        readonly ILogger<OutboxPublisherJob> _logger;
        readonly IServiceScopeFactory _scopeFactory;

        public OutboxPublisherJob(
            ILogger<OutboxPublisherJob> logger,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["Scope"] = nameof(OutboxPublisherJob)
            }))
            {
                _logger.LogInformation("OutboxPublisherJob started at {Time}.", DateTimeOffset.UtcNow);
                using var scope = _scopeFactory.CreateScope();
                var outboxMessageService = scope.ServiceProvider.GetRequiredService<IOutboxMessageService>();
                var messageBus = scope.ServiceProvider.GetRequiredService<IMessageBus>();
                var messages = await outboxMessageService.GetUnpublishedMessagesAsync(
                    takeSize: 20,
                    asTracking: true,
                    cancellationToken: context.CancellationToken);
                foreach (var message in messages)
                {
                    var type = Type.GetType(message.Type);
                    if (type == null)
                    {
                        _logger.LogError("Failed to resolve type '{Type}' for message ID {MessageId}.", message.Type, message.Id);
                        message.Error = "Invalid type.";
                        outboxMessageService.UpdateOutboxMessage(message);
                        continue;
                    }
                    var @event = JsonConvert.DeserializeObject(message.Content, type);
                    if (@event == null)
                    {
                        _logger.LogError("Failed to deserialize message with ID {MessageId} and Type {MessageType}.", message.Id, message.Type);
                        message.Error = "Deserialization failed.";
                        outboxMessageService.UpdateOutboxMessage(message);
                        continue;
                    }
                    try
                    {
                        await messageBus.PublishAsync(@event, type, context.CancellationToken);
                        message.ProcessedOn = DateTimeOffset.UtcNow;

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to publish message ID {MessageId}.", message.Id);
                        message.Error = ex.Message;
                    }
                }
                await outboxMessageService.SaveChangesAsync(context.CancellationToken);
                _logger.LogInformation("OutboxPublisherJob completed at {Time}. Processed {Count} messages.", DateTimeOffset.UtcNow, messages.Count);
            }
        }
    }
}
