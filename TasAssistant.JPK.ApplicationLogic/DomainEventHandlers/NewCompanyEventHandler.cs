using TaxAssistant.DDD.Abstraction;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Shared.Model.Domain;
using TaxAssistant.JPK.Shared.Model.Domain.Company;
using TaxAssistant.JPK.Shared.Model.Domain.Company.Events;

namespace TaxAssistant.JPK.ApplicationLogic.DomainEventHandlers
{
	public class NewCompanyEventHandler : IDomainEventHandler<NewCompanyEvent>
	{
		private readonly IRepository<Company> _repository;

		public NewCompanyEventHandler(IRepository<Company> repository)
		{
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
		}

		public async Task HandleAsync(NewCompanyEvent? domainEvent)
		{
			if (domainEvent!.TaxIdentificationNumber != null && await _repository.AnyAsync(x => x.TaxIdentificationNumber == domainEvent!.TaxIdentificationNumber))
			{
				return;
			}

			if (await _repository.AnyAsync(x => x.Name == domainEvent!.CompanyName))
			{
				return;
			}

			var company = new Company(Origin.JPK, domainEvent!.TaxIdentificationNumber, domainEvent.CompanyName);

			await _repository.AddAsync(company);
		}
	}
}
