using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RustRetail.IdentityService.Application.Abstractions.MessageBrokers;
using RustRetail.IdentityService.Infrastructure.MessageBrokers.RabbitMQ;

namespace RustRetail.IdentityService.Infrastructure.MessageBrokers
{
    internal static class MessageBrokersServiceCollectionExtensions
    {
        internal static IServiceCollection AddMessaging(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddRabbitMQ(configuration);
            services.AddScoped<IMessageSender, MassTransitMessageSender>();

            return services;
        }
    }
}
