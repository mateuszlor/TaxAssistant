using TaxAssistant.DDD.Abstraction;
using TaxAssistant.VatWhiteList.Model;

namespace TaxAssistant.JPK.Shared.Model.Domain.Company.Events
{
    public class CompanySynchronizedWithVatWhiteListEvent : IDomainEvent
    {
        public CompanySynchronizedWithVatWhiteListEvent(Guid companyId, Entity vatWhiteListData)
        {
            CompanyId = companyId;
            VatWhiteListData = vatWhiteListData;
        }

        public Guid CompanyId { get; }

        public Entity VatWhiteListData { get; }
    }
}