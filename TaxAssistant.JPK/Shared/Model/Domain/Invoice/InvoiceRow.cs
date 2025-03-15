
using TaxAssistant.JPK.Shared.Model.Database.Fa.Enum;

namespace TaxAssistant.JPK.Shared.Model.Domain.Invoice
{
    public class InvoiceRow : BaseDomainModel
    {
        public InvoiceRow(Origin origin, Guid invoiceId)
            : base(origin)
        {
            InvoiceId = invoiceId;
        }

        public virtual Invoice Invoice { get; set; }
        public Guid InvoiceId { get; set; }
        public string Name { get; set; }
        public string? MetricUnit { get; set; }
        public decimal? Count { get; set; }
        public VatRate VatRate { get; set; }
        public decimal UnitPriceNet { get; set; }
        public decimal UnitPriceGross { get; set; }
        public decimal TotalPriceNet { get; set; }
        public decimal TotalPriceGross { get; set; }
        public int Number { get; set; }
    }
}