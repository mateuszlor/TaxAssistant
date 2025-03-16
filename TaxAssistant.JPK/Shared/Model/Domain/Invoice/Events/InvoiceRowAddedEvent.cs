namespace TaxAssistant.JPK.Shared.Model.Domain.Invoice.Events
{
    public class InvoiceRowAddedEvent : BaseDomainEvent
    {
        public InvoiceRowAddedEvent(Guid id, int rowNumber) : base(id)
        {
            RowNumber = rowNumber;
        }

        public int RowNumber { get; }
    }
}