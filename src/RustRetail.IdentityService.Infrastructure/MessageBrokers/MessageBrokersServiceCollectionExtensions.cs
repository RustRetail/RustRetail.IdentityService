using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RustRetail.IdentityService.Infrastructure.MessageBrokers.RabbitMQ;
using RustRetail.SharedApplication.Behaviors.Messaging;

namespace RustRetail.IdentityService.Infrastructure.MessageBrokers
{
    internal static class MessageBrokersServiceCollectionExtensions
    {
        internal static IServiceCollection AddMessaging(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddRabbitMQ(configuration);
            services.AddScoped<IMessageBus, MassTransitMessageSender>();

            return services;
        }
    }
}
