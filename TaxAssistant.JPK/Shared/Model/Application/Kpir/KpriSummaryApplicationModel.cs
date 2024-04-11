namespace TaxAssistant.JPK.Shared.Model.Application.Kpir
{
    public class KpirSummaryApplicationModel : BaseApplicationModel
    {
        public Guid KpirId { get; set; }

        public virtual KpirApplicationModel Kpir { get; set; }

        public decimal PhysicalInventoryYearStart { get; set; }

        public decimal PhysicalInventoryYearEnd { get; set; }

        public decimal TotalCost { get; set; }

        public decimal TotalIncome { get; set; }
    }
}
