using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaxAssistant.JPK.Shared.Model.Database.Kpir.Enum;

namespace TaxAssistant.JPK.Shared.Model.Database.Kpir
{
    public class KpirRow : BaseModel
    {
        public Guid KpirId { get; set; }

        public virtual Kpir Kpir { get; set; }

        public int Number { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public string? DocumentNumber { get; set; }

        public string? CompanyData { get; set; }

        public string? CompanyAddress { get; set; }

        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? RevenueSold { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? RevenueOther { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? RevenueTotal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? CostGoods { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? CostGoodsRelated{ get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? CostSalaries { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? CostOther { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? CostTotal{ get; set; }

        public string? AdditionalField15 { get; set; }

        public string? ResearchAndDevelopmentDescription { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ResearchAndDevelopmentCost { get; set; }

        public string? AdditionalDescription { get; set; }

        public KpirType Type { get; set; } = KpirType.G;
    }
}
