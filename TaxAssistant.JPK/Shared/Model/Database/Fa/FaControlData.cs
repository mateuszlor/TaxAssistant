using System.ComponentModel.DataAnnotations.Schema;
using TaxAssistant.JPK.Shared.Model.Abstraction;

namespace TaxAssistant.JPK.Shared.Model.Database.Fa
{
	public class FaControlData : BaseModel
    {
        public Guid FaId { get; set; }

        public virtual Fa Fa { get; set; }

		public int InvoiceRowsCount { get; set; }

		public int InvoicesCount { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal TotalIncomeFromRows { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal TotalIncomeFromInvoices { get; set; }

		public int OrdersCount { get; internal set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal TotalIncomeFromOrders { get; internal set; }
	}
}
