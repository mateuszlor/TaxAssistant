namespace TaxAssistant.JPK.Shared.Model.Domain.Invoice
{
	internal class BuyerSetEvent : BaseCompanySetEvent
	{
		public BuyerSetEvent(Guid entityId, Company.Company company) : base(entityId, company) { }
	}
}