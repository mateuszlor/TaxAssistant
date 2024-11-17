using TaxAssistant.DDD.Abstraction;
using TaxAssistant.JPK.Shared.Model.Abstraction;

namespace TaxAssistant.JPK.Shared.Model.Domain
{
	public abstract class BaseDomainModel : BaseModel, IAggregate
	{		
		public IList<IDomainEvent> Events { get; } = [];

		public required Origin Origin { get; init; }

		protected BaseDomainModel(Origin origin)
		{
			Origin = origin;
		}
	}
}
