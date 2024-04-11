using TaxAssistant.JPK.Shared.Model.Application.Kpir;

namespace TaxAssistant.JPK.Shared.Model.Application
{
    public class ImportApplicationModel : BaseApplicationModel
    {        
        public Guid KpirId { get; set; }
        
        public virtual KpirApplicationModel? Kpir { get; set; }
    }
}
