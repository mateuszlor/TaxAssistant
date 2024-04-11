namespace TaxAssistant.JPK.Shared.Model.Application.Kpir
{
    public class KpirControlDataApplicationModel : BaseApplicationModel
    {
        public Guid KpirId { get; set; }

        public virtual KpirApplicationModel Kpir { get; set; }

        public int RowCount { get; set; }

        public decimal TotalIncome { get; set; }
    }
}
