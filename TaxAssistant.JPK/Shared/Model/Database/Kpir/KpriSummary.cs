using System.ComponentModel.DataAnnotations.Schema;

namespace TaxAssistant.JPK.Shared.Model.Database.Kpir
{
    public class KpirSummary : BaseModel
    {
        public Guid KpirId { get; set; }

        public virtual Kpir Kpir { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PhysicalInventoryYearStart { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PhysicalInventoryYearEnd { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalCost { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalIncome{ get; set; }
    }
}
