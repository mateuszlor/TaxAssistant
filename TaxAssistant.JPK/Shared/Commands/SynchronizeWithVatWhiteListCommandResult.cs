using TaxAssistant.JPK.Shared.Model.Domain.Company;

namespace TaxAssistant.JPK.Shared.Commands
{
    public class SynchronizeWithVatWhiteListCommandResult
    {
        public Company? Company { get; set; }

        public string? Error { get; set; }
    }
}
