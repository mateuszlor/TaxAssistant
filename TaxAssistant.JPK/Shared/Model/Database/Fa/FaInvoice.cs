using System.ComponentModel.DataAnnotations.Schema;

namespace TaxAssistant.JPK.Shared.Model.Database.Fa
{
	public class FaInvoice : BaseModel
	{
		public Guid FaId { get; set; }

		public virtual Fa Fa { get; set; }

		public string? DocumentNumber { get; set; }
		
		public virtual IList<FaInvoiceRow> Rows { get; set; }

		public string Currency { get; set; }

		public DateTime IssueDate { get; set; }

		public Guid? SellerId { get; set; }

		public virtual FaInvoiceCompany? Seller { get; set; }

		public Guid? BuyerId { get; set; }

		public virtual FaInvoiceCompany? Buyer { get; set; }

		public DateTime DeliveryDate { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal TotalPriceNetBaseRate { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal TotalVatBaseRate { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal? TotalVatBaseRateOtherCurrency { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal TotalPriceNetRate8 { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal TotalVatRate8 { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal? TotalVatRate8OtherCurrency { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal TotalPriceNetRate5 { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal TotalVatRate5 { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal? TotalVatRate5OtherCurrency { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal TotalPriceNetReverseCharge { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal TotalVatReverseCharge { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal? TotalVatReverseChargeOtherCurrency { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal TotalPriceNetForeignTransaction { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal TotalVatForeignTransaction { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal TotalPriceNetRate0 { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal TotalPriceNetVatExempted { get; set; }

		public bool CashAccountingScheme { get; set; }

		public bool IssuedByBuyer { get; set; }

		public bool ReversedCharge { get; set; }

		public bool SplitPayment { get; set; }

		public bool VatExemption { get; set; }

		public string? VatSubjectiveExemptionReason { get; set; }

		public string? VatObjectiveExemptionReason { get; set; }

		public string? VatObjectiveExemptionOtherReason { get; set; }

		public bool IssuedByExecutionProcedure { get; set; }

		public Guid? ExecutionProcedureIssuerId { get; set; }

		public virtual FaInvoiceCompany? ExecutionProcedureIssuer { get; set; }

		public bool IssuedByTaxRepresentative { get; set; }

		public Guid? TaxRepresentativeIssuerId { get; set; }

		public virtual FaInvoiceCompany? TaxRepresentativeIssuer { get; set; }

		public bool IntraCommunitySupplyOfGoodsForNewVechicle { get; set; }

		public DateTime? IntraCommunitySupplyOfGoodsForNewVechicleDateOfAprovalToUse { get; set; }

		public string? IntraCommunitySupplyOfGoodsForNewVechicleMileage { get; set; }

		public string? IntraCommunitySupplyOfGoodsForNewVechicleMotohours { get; set; }

		public bool IssuedByNextTaxpayer { get; set; }

		public bool VatMarginSchemeForTourism { get; set; }

		public bool VatMarginScheme { get; set; }

		public string? VatMarginSchemeType { get; set; }

		public InvoiceTypeEnum Type { get; set; }

		public string? CreditNoteReason { get; set; }

		public string? CreditNoteChangedDocumentNumber { get; set; }

		public string? CreditNoteChangedDocumentDate { get; set; }

		public string? AdvanceInvoiceNumber { get; set; }
	}
}
