namespace TaxAssistant.JPK.Shared.Model.Domain.Invoice.Events
{
    public class InvoiceCreatedEvent : BaseDomainEvent
    {
        public InvoiceCreatedEvent(Guid entityId) : base(entityId) { }
    }
}