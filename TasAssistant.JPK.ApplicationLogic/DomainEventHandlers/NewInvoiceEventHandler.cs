using TaxAssistant.DDD.Abstraction;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Shared.Model.Domain;
using TaxAssistant.JPK.Shared.Model.Domain.Company;
using TaxAssistant.JPK.Shared.Model.Domain.Events;
using TaxAssistant.JPK.Shared.Model.Domain.Invoice;

namespace TaxAssistant.JPK.ApplicationLogic.DomainEventHandlers
{
    public class NewInvoiceEventHandler : IDomainEventHandler<NewInvoiceEvent>
    {
        private readonly IRepository<Invoice> _invoiceRepository;
        private readonly IRepository<Company> _companyRepository;

        public NewInvoiceEventHandler(
            IRepository<Invoice> invoiceRepository,
            IRepository<Company> companyRepository)
        {
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        }

        public async Task HandleAsync(NewInvoiceEvent? domainEvent)
        {
            if (domainEvent == null)
            {
                return;
            }

            if (await _invoiceRepository.AnyAsync(x => x != null && x.Seller.TaxIdentificationNumber == domainEvent.SellerTaxIdentificationNumber && x.DocumentNumber == domainEvent.DocumentNumber))
            {
                return;
            }

            var companies = await _companyRepository.GetAllAsync(x => x.TaxIdentificationNumber == domainEvent.SellerTaxIdentificationNumber);
            var seller = companies.SingleOrDefault();

            var invoice = new Invoice(Origin.JPK, domainEvent.DocumentNumber);
            invoice.SetAmountValue(domainEvent.RevenueTotal!.Value);

            if (seller != null)
            {
                invoice.SetSeller(seller);
            }
            else
            {
                var sellerCompany = new Company(Origin.JPK, domainEvent.SellerTaxIdentificationNumber, null);
                await _companyRepository.AddAsync(sellerCompany);
                invoice.SetSeller(sellerCompany);
            }

            await _invoiceRepository.AddAsync(invoice);
        }
    }
}
