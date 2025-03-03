using TaxAssistant.DDD.Abstraction;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Shared.Model.Domain.Address;
using TaxAssistant.JPK.Shared.Model.Domain.Company;
using TaxAssistant.JPK.Shared.Model.Domain.Company.Events;

namespace TaxAssistant.JPK.ApplicationLogic.DomainEventHandlers
{
    public abstract class BaseNewCompanyWithAddressEventHandler : IDomainEventHandler<BaseNewCompanyWithAddressEvent>
    {
        private readonly IRepository<Company> _repository;

        protected BaseNewCompanyWithAddressEventHandler(IRepository<Company> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task HandleAsync(BaseNewCompanyWithAddressEvent? domainEvent)
        {
            if (domainEvent!.TaxIdentificationNumber != null && await _repository.AnyAsync(x => x.TaxIdentificationNumber == domainEvent!.TaxIdentificationNumber))
            {
                return;
            }

            if (await _repository.AnyAsync(x => x.Name == domainEvent!.CompanyName))
            {
                return;
            }

            var address = SplitAddress(domainEvent.Address!);

            var company = new Company(domainEvent.Origin, domainEvent!.TaxIdentificationNumber, domainEvent.CompanyName, address);

            await _repository.AddAsync(company);
        }

        private static Address? SplitAddress(string address)
        {
            // TODO: Implement address splitting
            return null;
        }
    }
}
