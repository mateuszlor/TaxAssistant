using TaxAssistant.DDD;

namespace TaxAssistant.JPK.Shared.Model.Domain.Invoice.Events
{
	internal class InvoiceCreatedEvent : BaseDomainEvent
	{
		public InvoiceCreatedEvent(Guid entityId) : base(entityId) { }
	}
}