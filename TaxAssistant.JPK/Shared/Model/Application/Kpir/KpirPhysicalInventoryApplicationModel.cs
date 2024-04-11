using TaxAssistant.JPK.Shared.Model.Application.Kpir.Enum;

namespace TaxAssistant.JPK.Shared.Model.Application.Kpir
{
    public class KpirPhysicalInventoryApplicationModel : BaseApplicationModel
    {
        public Guid KpirId { get; set; }

        public virtual KpirApplicationModel Kpir { get; set; }

        public DateTime Date { get; set; }

        public decimal Value { get; set; }

        public KpirTypeApplicationModel Type { get; set; } = KpirTypeApplicationModel.G;
    }
}
