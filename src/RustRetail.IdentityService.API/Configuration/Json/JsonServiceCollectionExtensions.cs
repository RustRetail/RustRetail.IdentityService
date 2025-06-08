using Microsoft.Extensions.Options;
using RustRetail.IdentityService.Domain.Enums;
using RustRetail.SharedKernel.Domain.Enums;
using System.Reflection;
using System.Text.Json.Serialization;

namespace RustRetail.IdentityService.API.Configuration.Json
{
    internal static class JsonServiceCollectionExtensions
    {
        internal static IServiceCollection AddJsonConfiguration(
            this IServiceCollection services)
        {
            services.AddEnumerationConverters();
            return services;
        }

        private static IServiceCollection AddEnumerationConverters(
            this IServiceCollection services)
        {
            var baseType = typeof(Enumeration);

            // Get Enum types only from domain assemblies
            var assemblies = new[] {
                Assembly.GetAssembly(typeof(ErrorType)),
                Assembly.GetAssembly(typeof(Gender)) };

            var enumTypes = assemblies
                .SelectMany(a => a!.GetTypes())
                .Where(t => t.IsSubclassOf(baseType) && !t.IsAbstract && !t.IsGenericType)
                .ToList();

            services.ConfigureHttpJsonOptions(options =>
            {
                foreach (var enumType in enumTypes)
                {
                    var converterType = typeof(EnumerationJsonConverter<>).MakeGenericType(enumType);
                    var converter = (JsonConverter)Activator.CreateInstance(converterType)!;
                    options.SerializerOptions.Converters.Add(converter);
                }
            });
            return services;
        }
    }
}
