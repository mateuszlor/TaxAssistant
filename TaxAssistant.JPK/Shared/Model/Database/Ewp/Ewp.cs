namespace TaxAssistant.JPK.Shared.Model.Database.Ewp
{
    public class Ewp : BaseModel
    {
        public virtual EwpHeader Header { get; set; }

        public virtual EwpCompany Subject { get; set; }

        public virtual ICollection<EwpRow> Rows { get; set; }

        public Guid RowsControlDataId { get; set; }

        public virtual EwpControlData RowsControlData { get; set; }

        public virtual ICollection<EwpFixedAsset>? FixedAssets { get; set; }

        public Guid? FixedAssetsControlDataId { get; set; }

        public virtual EwpControlData? FixedAssetsControlData { get; set; }
    }
}
