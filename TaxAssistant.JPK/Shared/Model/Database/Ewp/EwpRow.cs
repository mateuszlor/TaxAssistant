using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxAssistant.JPK.Shared.Model.Database.Ewp
{
    public class EwpRow : BaseModel
    {
        public Guid EwpId { get; set; }

        public virtual Ewp Ewp { get; set; }

        public int Number { get; set; }

        [DataType(DataType.Date)]
        public DateTime EntryDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime RevenueDate { get; set; }

        public string DocumentNumber { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? RevenueTaxed17Percent { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? RevenueTaxed15Percent { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? RevenueTaxed14Percent { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? RevenueTaxed12Point5Percent { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? RevenueTaxed12Percent { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? RevenueTaxed10Percent { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? RevenueTaxed8Point5Percent { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? RevenueTaxed5Point5Percent { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? RevenueTaxed3Percent { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? RevenueTotal { get; set; }

        public string? AdditionalDescription { get; set; }
    }
}
