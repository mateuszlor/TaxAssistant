
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxAssistant.JPK.Shared.Model.Database.Fa
{
	public class FaOrder : BaseModel
	{
		public Guid FaId { get; set; }

		public virtual Fa Fa { get; set; }

		public string InvoiceNumber { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Amount { get; set; }

		public virtual IList<FaOrderRow> Rows { get; set; }
	}
}