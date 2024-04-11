using TaxAssistant.JPK.Shared.Model.Application.Kpir.Enum;

namespace TaxAssistant.JPK.Shared.Model.Application.Kpir
{
    public class KpirHeaderApplicationModel : BaseApplicationModel
    {
        public Guid KpirId { get; set; }

        public virtual KpirApplicationModel Kpir { get; set; }

        public string FormCode { get; set; }

        public int FormVariant { get; set; }

        public KpirPurposeApplicationModel Purpose { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public string Currency { get; set; }

        public string TaxOfficeCode { get; set; }
    }
}
