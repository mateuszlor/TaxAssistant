using System.ComponentModel.DataAnnotations;
using TaxAssistant.JPK.Shared.Model.Database.Kpir.Enum;

namespace TaxAssistant.JPK.Shared.Model.Database.Fa
{
    public class FaHeader : BaseModel
    {
        public Guid FaId { get; set; }

        public virtual Fa Fa { get; set; }

        public string FormCode { get; set; }

        public int FormVariant { get; set; }

        public KpirPurpose Purpose { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateFrom { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateTo { get; set; }

        public string? Currency { get; set; }

        public string TaxOfficeCode { get; set; }

		[DataType(DataType.Date)]
		public DateTime GenerationDate { get; internal set; }
	}
}
