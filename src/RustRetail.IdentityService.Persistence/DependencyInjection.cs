using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RustRetail.IdentityService.Domain.Repositories;
using RustRetail.IdentityService.Persistence.Database;
using RustRetail.IdentityService.Persistence.Repositories;
using RustRetail.SharedKernel.Domain.Abstractions;
using RustRetail.SharedPersistence.Database;
using RustRetail.SharedPersistence.Interceptors;

namespace RustRetail.IdentityService.Persistence
{
    public static class DependencyInjection
    {
        const string ConnectionStringName = "IdentityDatabase";

        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddInterceptors()
                .AddDbContext(configuration)
                .AddUnitOfWork()
                .AddRepositories();

            return services;
        }

        private static IServiceCollection AddDbContext(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<IdentityDbContext>((serviceProvider, options) =>
            {
                var interceptor = serviceProvider.GetRequiredService<DomainEventHandlingInterceptor>();
                options.UseNpgsql(configuration.GetConnectionString(ConnectionStringName));
                options.AddInterceptors(interceptor);
            });

            return services;
        }

        private static IServiceCollection AddUnitOfWork(
            this IServiceCollection services)
        {
            services.AddScoped<IIdentityUnitOfWork, IdentityUnitOfWork>();

            return services;
        }

        private static IServiceCollection AddRepositories(
            this IServiceCollection services)
        {
            // Generic repository
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            // Custom repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            return services;
        }

        private static IServiceCollection AddInterceptors(
            this IServiceCollection services)
        {
            services.AddScoped<DomainEventHandlingInterceptor>();

            return services;
        }
    }
}
