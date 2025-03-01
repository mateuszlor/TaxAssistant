using Microsoft.Extensions.Logging;
using TaxAsistant.VatWhiteList.Client.Client;
using TaxAssistant.CQRS.Abstraction;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Shared.Commands;
using TaxAssistant.JPK.Shared.Model.Domain.Company;

namespace TaxAssistant.JPK.ApplicationLogic.CommandHandlers
{
    public class SynchronizeWithVatWhiteListCommandHandler : ICommandHandler<SynchronizeWithVatWhiteListCommand, SynchronizeWithVatWhiteListCommandResult>
    {
        private readonly ILogger<SynchronizeWithVatWhiteListCommandHandler> _logger;
        private readonly IRepository<Company> _repository;
        private readonly IVatWhiteListClient _vatWhiteListClient;

        public SynchronizeWithVatWhiteListCommandHandler(
            ILogger<SynchronizeWithVatWhiteListCommandHandler> logger,
            IRepository<Company> repository, 
            IVatWhiteListClient vatWhiteListClient)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _vatWhiteListClient = vatWhiteListClient ?? throw new ArgumentNullException(nameof(vatWhiteListClient));
        }

        public async Task<SynchronizeWithVatWhiteListCommandResult> HandleAsync(SynchronizeWithVatWhiteListCommand? command)
        {
            if (command == null)
            {
                throw new InvalidOperationException("Invalid command");
            }

            var company = await _repository.GetAsync(command!.CompanyId);

            if (company == null)
            {
                throw new InvalidOperationException("No company");
            }

            if (string.IsNullOrEmpty(company.TaxIdentificationNumber))
            {
                throw new InvalidOperationException("Cannot check company without Tax Identification Number");
            }

            if (company.VatWhiteListSynchronizationDate.HasValue && company.VatWhiteListSynchronizationDate.Value.Date == DateTime.UtcNow.Date)
            {
                throw new InvalidOperationException("Company already synchronized with VAT WhiteList");
            }

            var response = await _vatWhiteListClient.SearchByNip(company.TaxIdentificationNumber, DateTime.Today);

            if (response?.Result?.Subject == null)
            {
                _logger.LogWarning("Invalid response from VAT WhiteList: {@Response}", response);
                throw new InvalidOperationException("Invalid response from VAT WhiteList");
            }

            company.SynchronizeVatWhiteListData(response!.Result.Subject!);
            var updatedCompany = await _repository.UpdateAsync(company);

            return new SynchronizeWithVatWhiteListCommandResult
            {
                Company = updatedCompany
            };
        }
    }
}
