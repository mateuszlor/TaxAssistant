using TaxAssistant.DDD.Abstraction;

namespace TaxAssistant.JPK.Shared.Model.Domain.Company.Events
{
    public abstract class BaseNewCompanyEvent : IDomainEvent
    {
        protected BaseNewCompanyEvent()
        {
        }

        protected BaseNewCompanyEvent(string companyName, string taxIdentificationNumber)
        {
            CompanyName = companyName;
            TaxIdentificationNumber = taxIdentificationNumber;
        }

        public Origin Origin { get; } = Origin.JPK;
        public string CompanyName { get; init; }
		public string? TaxIdentificationNumber { get; init; }
    }
}
