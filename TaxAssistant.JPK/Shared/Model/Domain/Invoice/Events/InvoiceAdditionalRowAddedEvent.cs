namespace TaxAssistant.JPK.Shared.Model.Domain.Invoice.Events
{
    public class InvoiceAdditionalRowAddedEvent : BaseDomainEvent
    {
        public InvoiceAdditionalRowAddedEvent(Guid id, int rowNumber) : base(id)
        {
            RowNumber = rowNumber;
        }

        public int RowNumber { get; }
    }
}