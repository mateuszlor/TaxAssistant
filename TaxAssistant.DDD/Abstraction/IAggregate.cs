namespace TaxAssistant.DDD.Abstraction
{
	public interface IAggregate
	{
		public IList<IDomainEvent> Events { get; }
	}
}
