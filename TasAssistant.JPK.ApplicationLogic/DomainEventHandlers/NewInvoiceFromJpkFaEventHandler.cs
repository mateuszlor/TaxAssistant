using TaxAssistant.DDD.Abstraction;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Shared.Model.Domain;
using TaxAssistant.JPK.Shared.Model.Domain.Address;
using TaxAssistant.JPK.Shared.Model.Domain.Company;
using TaxAssistant.JPK.Shared.Model.Domain.Events;
using TaxAssistant.JPK.Shared.Model.Domain.Invoice;

namespace TaxAssistant.JPK.ApplicationLogic.DomainEventHandlers
{
    public class NewInvoiceFromJpkFaEventHandler : IDomainEventHandler<NewInvoiceFromJpkFaEvent>
    {
        private readonly IRepository<Invoice> _invoiceRepository;
        private readonly IRepository<Company> _companyRepository;

        public NewInvoiceFromJpkFaEventHandler(
            IRepository<Invoice> invoiceRepository,
            IRepository<Company> companyRepository)
        {
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        }

        public async Task HandleAsync(NewInvoiceFromJpkFaEvent? domainEvent)
        {
            if (domainEvent == null)
            {
                return;
            }

            if (await _invoiceRepository.AnyAsync(x => x != null && x.Seller != null && x.Seller.TaxIdentificationNumber == domainEvent.Seller.TaxIdentificationNumber && x.DocumentNumber == domainEvent.DocumentNumber))
            {
                return;
            }

            var invoice = new Invoice(Origin.JPK, domainEvent.DocumentNumber);

            var totalVat = domainEvent.InvoiceAmounts.TotalVatBaseRate + domainEvent.InvoiceAmounts.TotalVatRate8 + domainEvent.InvoiceAmounts.TotalVatRate5 + domainEvent.InvoiceAmounts.TotalVatReverseCharge;
            var totalGrossValue = domainEvent.InvoiceAmounts.TotalPriceNetVatExempted + totalVat;

            invoice.SetAmountValues(domainEvent.InvoiceAmounts.TotalPriceNetVatExempted, totalGrossValue, totalVat);
            invoice.SetIssueDate(domainEvent.IssueDate);
            invoice.SetDeliveryDate(domainEvent.DeliveryDate);
            invoice.SetSeller(await GetCompany(domainEvent.Seller));
            invoice.SetBuyer(await GetCompany(domainEvent.Buyer));

            domainEvent.Rows.Select(x => GetRow(domainEvent.Origin, x, invoice.Id)).ToList().ForEach(invoice.AddRow);

            await _invoiceRepository.AddAsync(invoice);
        }

        private Shared.Model.Domain.Invoice.InvoiceRow GetRow(Origin origin, Shared.Model.Domain.Events.InvoiceRow sourceRow, Guid invoiceId)
        {
            var row = new Shared.Model.Domain.Invoice.InvoiceRow(origin, invoiceId)
            {
                Name = sourceRow.Name,
                MetricUnit = sourceRow.MetricUnit,
                Count = sourceRow.Count,
                VatRate = sourceRow.VatRate!.Value,
                UnitPriceNet = sourceRow.UnitPriceNet,
                UnitPriceGross = sourceRow.UnitPriceGross,
                TotalPriceNet = sourceRow.TotalPriceNet,
                TotalPriceGross = sourceRow.TotalPriceGross!.Value
            };

            return row;
        }

        private async Task<Company> GetCompany(InvoiceCompany company)
        {
            var companies = await _companyRepository.GetAllAsync(x => x.TaxIdentificationNumber == company.TaxIdentificationNumber);
            var seller = companies.SingleOrDefault();

            if (seller == null)
            {                
                var sellerCompany = new Company(Origin.JPK, company.TaxIdentificationNumber, company.Name!, GetAddress(company.Address));
                seller = await _companyRepository.AddAsync(sellerCompany);
            }

            return seller;
        }

        private Address? GetAddress(string? address)
        {
            // TODO: address handling
            return null;
        }
    }
}
