using System.Text.Json.Serialization;
using TaxAssistant.JPK.Shared.Model.Domain.Invoice.Events;

namespace TaxAssistant.JPK.Shared.Model.Domain.Invoice
{
    [JsonSerializable(typeof(Invoice))]
    public class Invoice : BaseDomainModel
    {
        public Invoice(Origin origin, string documentNumber)
            : base(origin)
        {
            DocumentNumber = documentNumber;
            Events.Add(new InvoiceCreatedEvent(Id));
        }

        [JsonConstructor]
        public Invoice(Origin origin, string documentNumber, Guid? sellerId, Guid? buyerId, decimal totalNetValue, decimal totalGrossValue, decimal totalVat, bool vatDataSpecified, DateTime? issueDate, DateTime? deliveryDate)
            : base(origin)
        {
            DocumentNumber = documentNumber;
            SellerId = sellerId;
            BuyerId = buyerId;
            TotalNetValue = totalNetValue;
            TotalGrossValue = totalGrossValue;
            TotalVat = totalVat;
            VatDataSpecified = vatDataSpecified;
            IssueDate = issueDate;
            DeliveryDate = deliveryDate;
        }

        public string DocumentNumber { get; set; }
        public Guid? SellerId { get; set; }
        public virtual Company.Company? Seller { get; set; }
        public Guid? BuyerId { get; set; }
        public virtual Company.Company? Buyer { get; set; }
        public decimal TotalNetValue { get; set; }
        public decimal TotalGrossValue { get; set; }
        public decimal TotalVat { get; set; }
        public bool VatDataSpecified { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? DeliveryDate { get; set; }

        public virtual ICollection<InvoiceRow> Rows { get; set; } = new List<InvoiceRow>();
        public virtual ICollection<InvoiceAdditionalRow> AdditionalRows { get; set; } = new List<InvoiceAdditionalRow>();

        public void SetSeller(Company.Company company)
        {
            if (company == null)
            {
                throw new InvalidOperationException();
            }

            if (Seller == null)
            {
                Seller = company;

                Events.Add(new SellerSetEvent(Id, company));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void SetBuyer(Company.Company company)
        {
            if (company == null)
            {
                throw new InvalidOperationException();
            }

            if (Buyer == null)
            {
                Buyer = company;

                Events.Add(new BuyerSetEvent(Id, company));
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void SetAmountValues(decimal totalNetValue, decimal totalGrossValue, decimal totalVat)
        {
            if (totalNetValue == decimal.Zero)
            {
                throw new ArgumentException("TotalNetValue cannot be 0");
            }

            if (totalGrossValue == decimal.Zero)
            {
                throw new ArgumentException("TotalGrossValue cannot be 0");
            }

            if (totalVat != totalGrossValue - totalNetValue)
            {
                throw new ArgumentException("Net/gross/VAT amounts does not match");
            }

            TotalNetValue = totalNetValue;
            TotalGrossValue = totalGrossValue;
            TotalVat = totalVat;

            VatDataSpecified = true;

            Events.Add(new AmountValuesSetEvent(Id, VatDataSpecified));
        }

        public void SetAmountValue(decimal totalNetValue)
        {
            if (totalNetValue == decimal.Zero)
            {
                throw new ArgumentException("TotalNetValue cannot be 0");
            }

            TotalNetValue = totalNetValue;

            VatDataSpecified = false;

            Events.Add(new AmountValuesSetEvent(Id, VatDataSpecified));
        }

        public void AddRow(InvoiceRow row)
        {
            if (row.InvoiceId != Id)
            {
                throw new InvalidOperationException("Cannot assign row from other invoice");
            }

            row.Number = Rows.Count + 1;

            Rows.Add(row);
            Events.Add(new InvoiceRowAddedEvent(Id, row.Number));
        }

        public void AddAdditionalRow(InvoiceAdditionalRow row)
        {
            if (row.InvoiceId != Id)
            {
                throw new InvalidOperationException("Cannot assign additional row from other invoice");
            }

            row.Number = Rows.Count + 1;

            AdditionalRows.Add(row);
            Events.Add(new InvoiceAdditionalRowAddedEvent(Id, row.Number));
        }

        public void SetIssueDate(DateTime issueDate)
        {
            if (IssueDate != null)
            {
                throw new InvalidOperationException("Issue date already set");
            }

            IssueDate = issueDate;

            Events.Add(new IssueDateSetEvent(Id));
        }

        public void SetDeliveryDate(DateTime deliveryDate)
        {
            if (DeliveryDate != null)
            {
                throw new InvalidOperationException("Delivery date already set");
            }

            DeliveryDate = deliveryDate;

            Events.Add(new DeliveryDateSetEvent(Id));
        }
    }
}
