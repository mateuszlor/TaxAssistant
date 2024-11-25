using System.Text.Json.Serialization;
using TaxAssistant.DDD.Abstraction;
using TaxAssistant.JPK.Shared.Model.Abstraction;

namespace TaxAssistant.JPK.Shared.Model.Domain
{
	public abstract class BaseDomainModel : BaseModel, IAggregate
	{
		[JsonIgnore] 
		public IList<IDomainEvent> Events { get; } = [];

		public Origin Origin { get; init; }

		protected BaseDomainModel(Origin origin)
		{
			Origin = origin;
		}
	}
}
