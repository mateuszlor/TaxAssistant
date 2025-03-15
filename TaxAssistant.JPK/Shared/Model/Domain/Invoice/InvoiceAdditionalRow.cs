namespace TaxAssistant.JPK.Shared.Model.Domain.Invoice
{
    public class InvoiceAdditionalRow : BaseDomainModel
    {
        public InvoiceAdditionalRow(Origin origin, Guid invoiceId, string name, decimal amount)
            : base(origin)
        {
            InvoiceId = invoiceId;
            Name = name;
            Amount = amount;
        }

        public virtual Invoice Invoice { get; set; }
        public Guid InvoiceId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int Number { get; set; }
    }
}