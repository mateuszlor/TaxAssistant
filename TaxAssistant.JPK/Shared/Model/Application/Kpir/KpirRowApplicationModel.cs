using TaxAssistant.JPK.Shared.Model.Application.Kpir.Enum;

namespace TaxAssistant.JPK.Shared.Model.Application.Kpir
{
    public class KpirRowApplicationModel : BaseApplicationModel
    {
        public Guid KpirId { get; set; }

        public virtual KpirApplicationModel Kpir { get; set; }

        public int Number { get; set; }

        public DateTime Date { get; set; }

        public string? DocumentNumber { get; set; }

        public string? CompanyData { get; set; }

        public string? CompanyAddress { get; set; }

        public string? Description { get; set; }

        public decimal? RevenueSold { get; set; }

        public decimal? RevenueOther { get; set; }

        public decimal? RevenueTotal { get; set; }

        public decimal? CostGoods { get; set; }

        public decimal? CostGoodsRelated { get; set; }

        public decimal? CostSalaries { get; set; }

        public decimal? CostOther { get; set; }

        public decimal? CostTotal { get; set; }

        public string? AdditionalField15 { get; set; }

        public string? ResearchAndDevelopmentDescription { get; set; }

        public decimal? ResearchAndDevelopmentCost { get; set; }

        public string? AdditionalDescription { get; set; }

        public KpirTypeApplicationModel Type { get; set; } = KpirTypeApplicationModel.G;
    }
}
