using TaxAssistant.DDD.Abstraction;

namespace TaxAssistant.JPK.Shared.Model.Domain
{
	public abstract class BaseDomainEvent : IDomainEvent
	{
		protected BaseDomainEvent(Guid entityId)
		{
			EntityId = entityId;
		}

		public Guid EntityId { get; set; }
	}
}
