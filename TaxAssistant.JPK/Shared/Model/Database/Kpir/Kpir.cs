namespace TaxAssistant.JPK.Shared.Model.Database.Kpir
{
    public class Kpir : BaseModel
    {
        public virtual ICollection<KpirRow> Rows { get; set; }

        public virtual ICollection<KpirPhysicalInventory> PhysicalInventories { get; set; }

        public virtual KpirSummary Summary { get; set; }

        public virtual KpirHeader Header { get; set; }

        public virtual KpirControlData ControlData { get; set; }

        public virtual KpirCompany Subject { get; set; }
    }
}
