using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaxAssistant.JPK.Shared.Model.Database.Kpir.Enum;

namespace TaxAssistant.JPK.Shared.Model.Database.Kpir
{
    public class KpirPhysicalInventory : BaseModel
    {
        public Guid KpirId { get; set; }

        public virtual Kpir Kpir { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }

        public KpirType Type { get; set; } = KpirType.G;
    }
}
