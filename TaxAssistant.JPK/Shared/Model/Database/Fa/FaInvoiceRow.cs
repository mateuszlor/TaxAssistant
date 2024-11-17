using System.ComponentModel.DataAnnotations.Schema;
using TaxAssistant.JPK.Shared.Model.Abstraction;
using TaxAssistant.JPK.Shared.Model.Database.Fa.Enum;

namespace TaxAssistant.JPK.Shared.Model.Database.Fa
{
	public class FaInvoiceRow : BaseModel
	{
		public Guid InvoiceId { get; set; }

		public virtual FaInvoice Invoice { get; set; }

		public int Number { get; set; }

		public string? DocumentNumber { get; set; }

		public string Name { get; set; }

		public string? MetricUnit { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal? Count { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal UnitPriceNet { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal UnitPriceGross { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Discount { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal TotalPriceNet { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal? TotalPriceGross { get; set; }

		public VatRate? VatRate { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal? VatRateSpecial { get; set; }
	}
}
