using TaxAssistant.DDD.Abstraction;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Shared.Model.Domain.Address;
using TaxAssistant.JPK.Shared.Model.Domain.Company;
using TaxAssistant.JPK.Shared.Model.Domain.Company.Events;

namespace TaxAssistant.JPK.ApplicationLogic.DomainEventHandlers
{
    public class NewCompanyFromJpkEwpEventHandler : IDomainEventHandler<NewCompanyFromJpkEwpEvent>
    {
        private readonly IRepository<Company> _repository;

        public NewCompanyFromJpkEwpEventHandler(IRepository<Company> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task HandleAsync(NewCompanyFromJpkEwpEvent? domainEvent)
        {
            if (domainEvent!.TaxIdentificationNumber != null && await _repository.AnyAsync(x => x.TaxIdentificationNumber == domainEvent!.TaxIdentificationNumber))
            {
                return;
            }

            if (await _repository.AnyAsync(x => x.Name == domainEvent!.CompanyName))
            {
                return;
            }

            var address = new Address(domainEvent.Origin, string.Empty, domainEvent.PostalCode, domainEvent.City, domainEvent.Street, domainEvent.BuildingNumber, domainEvent.LocalNumber, domainEvent.Voivodeship);

            var company = new Company(domainEvent.Origin, domainEvent!.TaxIdentificationNumber, domainEvent.CompanyName, address);

            await _repository.AddAsync(company);
        }
    }
}
