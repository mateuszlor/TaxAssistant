using TaxAssistant.DDD.Abstraction;

namespace TaxAssistant.JPK.Shared.Model.Domain.Events
{
    public class NewInvoiceFromJpkEwpEvent : IDomainEvent
    {
        public NewInvoiceFromJpkEwpEvent(
            string sellerTaxIdentificationNumber,
            string documentNumber, 
            string? additionalDescription,
            DateTime entryDate, 
            DateTime revenueDate,
            decimal? revenueTotal, 
            decimal? revenueTaxed3Percent,
            decimal? revenueTaxed5Point5Percent,
            decimal? revenueTaxed8Point5Percent, 
            decimal? revenueTaxed10Percent, 
            decimal? revenueTaxed12Percent, 
            decimal? revenueTaxed12Point5Percent, 
            decimal? revenueTaxed14Percent, 
            decimal? revenueTaxed15Percent, 
            decimal? revenueTaxed17Percent)
        {
            SellerTaxIdentificationNumber = sellerTaxIdentificationNumber;
            DocumentNumber = documentNumber;
            AdditionalDescription = additionalDescription;
            EntryDate = entryDate;
            RevenueDate = revenueDate;
            RevenueTotal = revenueTotal;
            RevenueTaxed3Percent = revenueTaxed3Percent;
            RevenueTaxed5Point5Percent = revenueTaxed5Point5Percent;
            RevenueTaxed8Point5Percent = revenueTaxed8Point5Percent;
            RevenueTaxed10Percent = revenueTaxed10Percent;
            RevenueTaxed12Percent = revenueTaxed12Percent;
            RevenueTaxed12Point5Percent = revenueTaxed12Point5Percent;
            RevenueTaxed14Percent = revenueTaxed14Percent;
            RevenueTaxed15Percent = revenueTaxed15Percent;
            RevenueTaxed17Percent = revenueTaxed17Percent;
        }

        public Origin Origin { get; } = Origin.JPK;
        public string SellerTaxIdentificationNumber { get; }
        public string DocumentNumber { get; }
        public string? AdditionalDescription { get; }
        public DateTime EntryDate { get; }
        public DateTime RevenueDate { get; }
        public decimal? RevenueTotal { get; }
        public decimal? RevenueTaxed3Percent { get; }
        public decimal? RevenueTaxed5Point5Percent { get; }
        public decimal? RevenueTaxed8Point5Percent { get; }
        public decimal? RevenueTaxed10Percent { get; }
        public decimal? RevenueTaxed12Percent { get; }
        public decimal? RevenueTaxed12Point5Percent { get; }
        public decimal? RevenueTaxed14Percent { get; }
        public decimal? RevenueTaxed15Percent { get; }
        public decimal? RevenueTaxed17Percent { get; }
    }
}
