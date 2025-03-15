using TaxAssistant.JPK.Shared.Model.Database.Fa.Enum;

namespace TaxAssistant.JPK.Shared.Model.Domain.Events
{
    public class InvoiceRow
    {
        public InvoiceRow(string name, string? metricUnit, decimal? count, VatRate? vatRate, decimal unitPriceNet, decimal unitPriceGross, decimal totalPriceNet, decimal? totalPriceGross)
        {
            Name = name;
            MetricUnit = metricUnit;
            Count = count;
            VatRate = vatRate;
            UnitPriceNet = unitPriceNet;
            UnitPriceGross = unitPriceGross;
            TotalPriceNet = totalPriceNet;
            TotalPriceGross = totalPriceGross;
        }

        public string Name { get; }
        public string? MetricUnit { get; }
        public decimal? Count { get; }
        public VatRate? VatRate { get; }
        public decimal UnitPriceNet { get; }
        public decimal UnitPriceGross { get; }
        public decimal TotalPriceNet { get; }
        public decimal? TotalPriceGross { get; }
    }
}