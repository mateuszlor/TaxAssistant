using Microsoft.Extensions.Logging;
using TaxAssistant.DDD.Abstraction;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Database;
using TaxAssistant.JPK.Shared.Model.Database.Fa;
using TaxAssistant.JPK.Shared.Model.Domain;
using TaxAssistant.JPK.Shared.Model.Domain.Company.Events;
using TaxAssistant.JPK.Shared.Model.Domain.Events;

namespace TaxAssistant.JPK.ApplicationLogic.Repository
{
    public class FaRepository : BaseRepository<Fa>
    {
        public FaRepository(DatabaseContext databaseContext, IDomainEventDispatcher dispatcher, ILogger<FaRepository> logger)
            : base(databaseContext, dispatcher, logger)
        {
        }

        public override async Task<Fa> AddAsync(Fa item)
        {
            var events = item
                .Invoices
                ?.Select(x => new NewCompanyFromJpkFaEvent(x.Seller!.Name!, x.Seller.TaxIdentificationNumber!, x.Seller.Address!))
                .Distinct()
                .ToList()
                ?? [];

            events.ForEach(x => item.Events.Add(x));

            events = item
                .Invoices
                ?.Select(x => new NewCompanyFromJpkFaEvent(x.Buyer!.Name!, x.Buyer.TaxIdentificationNumber!, x.Buyer.Address!))
                .Distinct()
                .ToList()
                ?? [];

            events.ForEach(x => item.Events.Add(x));

            var newInvoiceEvents = item
                .Invoices
                ?.Select(x => new NewInvoiceFromJpkFaEvent(
                    new InvoiceCompany(x.Seller!.TaxIdentificationNumber, x.Seller.Name, x.Seller.Address),
                    new InvoiceCompany(x.Buyer!.TaxIdentificationNumber, x.Buyer.Name, x.Buyer.Address!), 
                    x.DocumentNumber!,
                    x.IssueDate,
                    x.DeliveryDate,
                    x.ReversedCharge,
                    x.SplitPayment,
                    new InvoiceAmounts(
                        x.TotalPriceNetVatExempted, x.TotalPriceNetRate0, x.TotalPriceNetRate5, x.TotalPriceNetRate8, x.TotalPriceNetBaseRate, x.TotalPriceNetForeignTransaction, x.TotalPriceNetReverseCharge,
                        x.TotalVatRate5, x.TotalVatRate5OtherCurrency, x.TotalVatRate8, x.TotalVatRate8OtherCurrency, x.TotalVatBaseRate, x.TotalVatBaseRateOtherCurrency, x.TotalVatReverseCharge, x.TotalVatReverseChargeOtherCurrency),
                    x.Rows.Select(r => new InvoiceRow(r.Name, r.MetricUnit, r.Count, r.VatRate, r.UnitPriceNet, r.UnitPriceGross, r.TotalPriceNet, r.TotalPriceGross)).ToList()))
                .Distinct()
                .ToList()
                ?? [];

            newInvoiceEvents.ForEach(item.Events.Add);

            var result = await base.AddAsync(item);

            return result;
        }
    }
}
