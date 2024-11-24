namespace TaxAssistant.JPK.Shared.Model.Domain.Invoice
{
	public abstract class BaseCompanySetEvent : BaseDomainEvent
	{
		protected BaseCompanySetEvent(Guid entityId, Company.Company company) : base(entityId)
		{
			Company = company;
		}

		public Company.Company Company { get; }
	}
}