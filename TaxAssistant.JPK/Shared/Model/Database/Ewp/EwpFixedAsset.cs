using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxAssistant.JPK.Shared.Model.Database.Ewp
{
    public class EwpFixedAsset : BaseModel
    {
        public Guid EwpId { get; set; }

        public virtual Ewp Ewp { get; set; }

        public int Number { get; set; }

        [DataType(DataType.Date)]
        public DateTime TransferDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime AcceptanceDate { get; set; }

        public string DocumentNumber { get; set; }

        public string Description { get; set; }

        public string CategoryCode { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? InitialValue { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? DepreciationRate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? UpdatedInitialValue { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
