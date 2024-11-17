namespace TaxAssistant.DDD.Abstraction
{
	public interface IDomainEventHandler<in TIn>
		where TIn : IDomainEvent
	{
		Task HandleAsync(TIn? command);
	}
}
