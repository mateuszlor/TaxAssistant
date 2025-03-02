using TaxAssistant.JPK.Shared.Model.Domain.Invoice.Events;

namespace TaxAssistant.JPK.Shared.Model.Domain.Invoice
{
	public class Invoice : BaseDomainModel
	{
		public Invoice(Origin origin, string documentNumber)
			: base(origin)
		{
			DocumentNumber = documentNumber;
			Events.Add(new InvoiceCreatedEvent(Id));
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
    }
}
