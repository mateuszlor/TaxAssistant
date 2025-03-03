using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Shared.Model.Domain.Company;
using TaxAssistant.JPK.Shared.Model.Domain.Company.Events;

namespace TaxAssistant.JPK.ApplicationLogic.DomainEventHandlers
{
    public class NewCompanyFromJpkFaEventHandler(IRepository<Company> repository) : BaseNewCompanyWithAddressEventHandler<NewCompanyFromJpkFaEvent>(repository)
    {
    }
}
