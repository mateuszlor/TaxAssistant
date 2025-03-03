using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Shared.Model.Domain.Company;

namespace TaxAssistant.JPK.ApplicationLogic.DomainEventHandlers
{
    public class NewCompanyFromJpkEwpEventHandler(IRepository<Company> repository) : BaseNewCompanyWithAddressEventHandler(repository)
    {
    }
}
