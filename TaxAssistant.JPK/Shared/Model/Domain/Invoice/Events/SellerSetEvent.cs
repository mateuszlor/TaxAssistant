namespace TaxAssistant.JPK.Shared.Model.Domain.Invoice.Events
{
	internal class SellerSetEvent : BaseCompanySetEvent
	{
		public SellerSetEvent(Guid entityId, Company.Company company) : base(entityId, company) { }
	}
}