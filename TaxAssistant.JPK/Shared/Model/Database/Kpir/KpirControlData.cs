using System.ComponentModel.DataAnnotations.Schema;

namespace TaxAssistant.JPK.Shared.Model.Database.Kpir
{
    public class KpirControlData : BaseModel
    {
        public Guid KpirId { get; set; }

        public virtual Kpir Kpir { get; set; }

        public int RowCount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalIncome { get; set; }
    }
}
