using TaxAssistant.DDD.Abstraction;

namespace TaxAssistant.JPK.Shared.Model.Domain.Events
{
    public class NewInvoiceFromJpkFaEvent : IDomainEvent
    {
        public NewInvoiceFromJpkFaEvent(InvoiceCompany seller, InvoiceCompany buyer, string documentNumber, DateTime issueDate, DateTime deliveryDate, bool reversedCharge, bool splitPayment, InvoiceAmounts invoiceAmounts, IList<InvoiceRow> rows)
        {
            Seller = seller;
            Buyer = buyer;
            DocumentNumber = documentNumber;
            IssueDate = issueDate;
            DeliveryDate = deliveryDate;
            ReversedCharge = reversedCharge;
            SplitPayment = splitPayment;
            InvoiceAmounts = invoiceAmounts;
            Rows = rows;
        }

        public Origin Origin { get; } = Origin.JPK;
        public InvoiceCompany Seller { get; }
        public InvoiceCompany Buyer { get; }
        public string DocumentNumber { get; }
        public DateTime IssueDate { get; }
        public DateTime DeliveryDate { get; }
        public bool ReversedCharge { get; }
        public bool SplitPayment { get; }
        public InvoiceAmounts InvoiceAmounts { get; }
        public IList<InvoiceRow> Rows { get; }
    }
}
