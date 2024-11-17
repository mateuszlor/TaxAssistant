namespace TaxAssistant.DDD.Abstraction
{
	public interface IDomainEventDispatcher
	{
		Task DispatchAsync<T>(T e) where T : IDomainEvent;

		Task DispatchAllAsync<T>(IEnumerable<T> events) where T : IDomainEvent;
	}
}
