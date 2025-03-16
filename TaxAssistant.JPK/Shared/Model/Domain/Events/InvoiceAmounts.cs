namespace TaxAssistant.JPK.Shared.Model.Domain.Events
{
    public class InvoiceAmounts
    {
        public decimal TotalPriceNetVatExempted { get; }
        public decimal TotalPriceNetRate0 { get; }
        public decimal TotalPriceNetRate5 { get; }
        public decimal TotalPriceNetRate8 { get; }
        public decimal TotalPriceNetBaseRate { get; }
        public decimal TotalPriceNetForeignTransaction { get; }
        public decimal TotalPriceNetReverseCharge { get; }
        public decimal TotalVatRate5 { get; }
        public decimal? TotalVatRate5OtherCurrency { get; }
        public decimal TotalVatRate8 { get; }
        public decimal? TotalVatRate8OtherCurrency { get; }
        public decimal TotalVatBaseRate { get; }
        public decimal? TotalVatBaseRateOtherCurrency { get; }
        public decimal TotalVatReverseCharge { get; }
        public decimal? TotalVatReverseChargeOtherCurrency { get; }

        public InvoiceAmounts(decimal totalPriceNetVatExempted, decimal totalPriceNetRate0, decimal totalPriceNetRate5, decimal totalPriceNetRate8, decimal totalPriceNetBaseRate, decimal totalPriceNetForeignTransaction, decimal totalPriceNetReverseCharge, decimal totalVatRate5, decimal? totalVatRate5OtherCurrency, decimal totalVatRate8, decimal? totalVatRate8OtherCurrency, decimal totalVatBaseRate, decimal? totalVatBaseRateOtherCurrency, decimal totalVatReverseCharge, decimal? totalVatReverseChargeOtherCurrency)
        {
            TotalPriceNetVatExempted = totalPriceNetVatExempted;
            TotalPriceNetRate0 = totalPriceNetRate0;
            TotalPriceNetRate5 = totalPriceNetRate5;
            TotalPriceNetRate8 = totalPriceNetRate8;
            TotalPriceNetBaseRate = totalPriceNetBaseRate;
            TotalPriceNetForeignTransaction = totalPriceNetForeignTransaction;
            TotalPriceNetReverseCharge = totalPriceNetReverseCharge;
            TotalVatRate5 = totalVatRate5;
            TotalVatRate5OtherCurrency = totalVatRate5OtherCurrency;
            TotalVatRate8 = totalVatRate8;
            TotalVatRate8OtherCurrency = totalVatRate8OtherCurrency;
            TotalVatBaseRate = totalVatBaseRate;
            TotalVatBaseRateOtherCurrency = totalVatBaseRateOtherCurrency;
            TotalVatReverseCharge = totalVatReverseCharge;
            TotalVatReverseChargeOtherCurrency = totalVatReverseChargeOtherCurrency;
        }
    }
}