using TaxAssistant.CQRS.Abstraction;

namespace TaxAssistant.JPK.Shared.Commands
{
	public class SynchronizeWithVatWhiteListCommand : ICommand
    {
        public SynchronizeWithVatWhiteListCommand(Guid companyId)
        {
            CompanyId = companyId;
        }

        public Guid CompanyId { get; }
    }
}
