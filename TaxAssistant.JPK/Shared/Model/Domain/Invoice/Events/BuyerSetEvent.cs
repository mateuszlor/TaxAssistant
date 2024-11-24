namespace TaxAssistant.JPK.Shared.Model.Domain.Invoice.Events
{
    public class BuyerSetEvent : BaseCompanySetEvent
    {
        public BuyerSetEvent(Guid entityId, Company.Company company) : base(entityId, company) { }
    }
}