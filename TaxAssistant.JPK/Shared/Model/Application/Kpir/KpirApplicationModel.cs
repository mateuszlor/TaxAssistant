namespace TaxAssistant.JPK.Shared.Model.Application.Kpir
{
    public class KpirApplicationModel : BaseApplicationModel
    { 
        public virtual ICollection<KpirRowApplicationModel> Rows { get; set; }

        public virtual ICollection<KpirPhysicalInventoryApplicationModel> PhysicalInventories { get; set; }

        public virtual KpirSummaryApplicationModel Summary { get; set; }

        public virtual KpirHeaderApplicationModel Header { get; set; }

        public virtual KpirControlDataApplicationModel ControlData { get; set; }
    }
}
