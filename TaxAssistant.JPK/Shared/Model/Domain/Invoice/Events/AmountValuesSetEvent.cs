using TaxAssistant.DDD;

namespace TaxAssistant.JPK.Shared.Model.Domain.Invoice.Events
{
	internal class AmountValuesSetEvent : BaseDomainEvent
	{
		public AmountValuesSetEvent(Guid id) : base(id) { }
	}
}