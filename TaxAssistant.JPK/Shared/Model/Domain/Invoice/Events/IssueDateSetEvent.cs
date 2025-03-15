namespace TaxAssistant.JPK.Shared.Model.Domain.Invoice.Events
{
    public class IssueDateSetEvent : BaseDomainEvent
    {
        public IssueDateSetEvent(Guid id) : base(id)
        {
        }
    }
}