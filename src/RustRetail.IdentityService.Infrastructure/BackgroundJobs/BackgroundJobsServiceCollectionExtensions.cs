using Microsoft.Extensions.DependencyInjection;
using Quartz;
using RustRetail.IdentityService.Infrastructure.BackgroundJobs.Quartz;

namespace RustRetail.IdentityService.Infrastructure.BackgroundJobs
{
    internal static class BackgroundJobsServiceCollectionExtensions
    {
        const string OutboxPublisherJobKey = "OutboxPublisherJob";

        internal static IServiceCollection AddBackgroundJobs(
            this IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                q.AddJob<OutboxPublisherJob>(options => options.WithIdentity(OutboxPublisherJobKey));
                q.AddTrigger(options =>
                {
                    options.ForJob(OutboxPublisherJobKey)
                        .WithIdentity($"{OutboxPublisherJobKey}Trigger")
                        .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever())
                        .WithDescription("Trigger for Outbox Publisher Job");
                });
            });
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            return services;
        }
    }
}
