namespace RustRetail.IdentityService.Application.Abstractions.MessageBrokers
{
    public interface IMessageSender
    {
        Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
            where T : class;
    }
}
