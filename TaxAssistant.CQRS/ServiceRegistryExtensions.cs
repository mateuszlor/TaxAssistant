using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TaxAssistant.CQRS
{
    public static class ServiceRegistryExtensions
    {
        public static void AddCqrs(this IServiceCollection services)
        {
            services.AddScoped<Gate>();
        }

        public static void AddCommandHandlers(this IServiceCollection services)
        {
            var handlerInarfaces = new[]
            {
                typeof(ICommandHandler<>),
                typeof(ICommandHandler<,>)
            };

            var handlers = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x
                    .GetTypes()
                    .Where(t => t
                        .GetInterfaces()
                        .Any(i => i.IsGenericType && handlerInarfaces.Contains(i.GetGenericTypeDefinition()))));

            foreach (var handler in handlers)
            {
                var handlerInterface = handler
                    .GetInterfaces()
                    .First(i => i.IsGenericType && handlerInarfaces.Contains(i.GetGenericTypeDefinition()));

                services.AddScoped(handlerInterface, handler);
            }
        }
    }
}