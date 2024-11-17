using Microsoft.Extensions.DependencyInjection;
using TaxAssistant.DDD.Abstraction;

namespace TaxAssistant.DDD
{
	public class DomainEventDispatcher : IDomainEventDispatcher
	{
		private readonly IServiceProvider _serviceProvider;

		public DomainEventDispatcher(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
		}

		public async Task DispatchAsync<T>(T e)
			where T : IDomainEvent
		{
			var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(e.GetType());
			var handlers = _serviceProvider.GetServices(handlerType);

			foreach (var handler in handlers)
			{
				var methodName = nameof(IDomainEventHandler<T>.HandleAsync);
				var method = handler!.GetType().GetMethod(methodName);

				if (method == null)
				{
					throw new Exception($"Handler {handler.GetType().FullName} is invalid");
				}

				var task = method.Invoke(handler, [e]) as Task;

				await task!;
			}
		}

		public async Task DispatchAllAsync<T>(IEnumerable<T> events)
			where T : IDomainEvent
		{
			foreach (var e in events)
			{
				await DispatchAsync(e);
			}
		}
	}
}
