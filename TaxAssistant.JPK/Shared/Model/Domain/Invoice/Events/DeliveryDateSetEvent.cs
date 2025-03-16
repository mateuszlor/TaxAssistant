namespace TaxAssistant.JPK.Shared.Model.Domain.Invoice.Events
{
    public class DeliveryDateSetEvent : BaseDomainEvent
    {
        public DeliveryDateSetEvent(Guid id) : base(id)
        {
        }
    }
}