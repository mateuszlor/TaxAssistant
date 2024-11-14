using System.ComponentModel.DataAnnotations.Schema;
using TaxAssistant.JPK.Shared.Model.Database.Fa.Enum;

namespace TaxAssistant.JPK.Shared.Model.Database.Fa
{
    public class FaOrderRow : BaseModel
	{
		public Guid OrderId { get; set; }

		public virtual FaOrder Order { get; set; }

		public string Name { get; internal set; }

		public string MetricUnit { get; internal set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal? Count { get; internal set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal? UnitPriceNet { get; internal set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal? TotalPriceNet { get; internal set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal? TotalVat { get; internal set; }

		public VatRate? VatRate { get; internal set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal? VatSpecialRate { get; internal set; }
	}
}