using Microsoft.Extensions.DependencyInjection;
using TaxAssistant.DDD.Abstraction;

namespace TaxAssistant.DDD
{
	public static class ServiceRegistryExtensions
	{
		public static void AddDDD(this IServiceCollection services)
		{
			services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
		}

		public static void AddDomainEventHandlers(this IServiceCollection services)
		{
			var handlerInarfaces = new[]
			{
				typeof(IDomainEventHandler<>)
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