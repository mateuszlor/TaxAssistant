using System.ComponentModel.DataAnnotations.Schema;
using TaxAssistant.JPK.Shared.Model.Abstraction;

namespace TaxAssistant.JPK.Shared.Model.Database.Ewp
{
	public class EwpControlData : BaseModel
	{
		public Guid EwpId { get; set; }

		public virtual Ewp Ewp { get; set; }

		public int RowCount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalValue { get; set; }
    }
}
