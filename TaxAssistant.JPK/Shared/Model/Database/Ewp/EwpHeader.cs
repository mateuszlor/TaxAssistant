using System.ComponentModel.DataAnnotations;
using TaxAssistant.JPK.Shared.Model.Database.Ewp.Enum;

namespace TaxAssistant.JPK.Shared.Model.Database.Ewp
{
    public class EwpHeader : BaseModel
    {
        public Guid EwpId { get; set; }

        public virtual Ewp Ewp { get; set; }

        public string FormCode { get; set; }

        public int FormVariant { get; set; }

        public EwpPurpose Purpose { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateFrom { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateTo { get; set; }

        public string Currency { get; set; }

        public string TaxOfficeCode { get; set; }
    }
}
