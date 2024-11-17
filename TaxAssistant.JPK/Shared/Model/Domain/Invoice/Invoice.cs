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

		public Origin Orogin { get; }
		public required string DocumentNumber { get; set; }
		public Company.Company? Seller { get; set; }
		public Company.Company? Byuer { get; set; }
		public decimal TotalNetValue { get; set; }
		public decimal TotalGrossValue { get; set; }
		public decimal TotalVat { get; set; }

		public void SetSeller(Company.Company company)
		{
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

		public void SetByuer(Company.Company company)
		{
			if (Byuer == null)
			{
				Byuer = company;

				Events.Add(new BuyerSetEvent(Id, company));
			}
			else
			{
				throw new InvalidOperationException();
			}
		}

		public void SetAmountValues(decimal totalNetValue, decimal totalGrossValue, decimal totalVat)
		{
			if (TotalNetValue != decimal.Zero || TotalGrossValue != decimal.Zero || TotalVat != decimal.Zero)
			{
				throw new InvalidOperationException();
			}

			if (totalVat != totalGrossValue - totalNetValue)
			{
				throw new ArgumentException("Invalid amounts");
			}

			TotalNetValue = totalNetValue;
			TotalGrossValue = totalGrossValue;
			TotalVat = totalVat;

			Events.Add(new AmountValuesSetEvent(Id));
		}
	}
}
