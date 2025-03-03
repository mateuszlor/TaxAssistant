using Microsoft.Extensions.Logging;
using TaxAssistant.DDD.Abstraction;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Database;
using TaxAssistant.JPK.Shared.Model.Database.Ewp;
using TaxAssistant.JPK.Shared.Model.Domain;
using TaxAssistant.JPK.Shared.Model.Domain.Company.Events;
using TaxAssistant.JPK.Shared.Model.Domain.Events;

namespace TaxAssistant.JPK.ApplicationLogic.Repository
{
    public class EwpRepository : BaseRepository<Ewp>
    {
        public EwpRepository(DatabaseContext databaseContext, IDomainEventDispatcher dispatcher, ILogger<EwpRepository> logger)
            : base(databaseContext, dispatcher, logger)
        {
        }

        public override async Task<Ewp> AddAsync(Ewp item)
        {
            item
                .FixedAssets?
                .Select(x => new NewFixedAssetEvent(x.CategoryCode, x.Description, x.DocumentNumber, x.TransferDate, x.AcceptanceDate, x.InitialValue, x.UpdatedInitialValue))
                .Distinct()
                .ToList()
                .ForEach(item.Events.Add);

            item
                .Rows?
                .Select(x => new NewInvoiceEvent(Origin.JPK, item.Subject.TaxIdentificationNumber, x.DocumentNumber, x.AdditionalDescription, x.EntryDate, x.RevenueDate, x.RevenueTotal, x.RevenueTaxed3Percent, x.RevenueTaxed5Point5Percent, x.RevenueTaxed8Point5Percent, x.RevenueTaxed10Percent, x.RevenueTaxed12Percent, x.RevenueTaxed12Point5Percent, x.RevenueTaxed14Percent, x.RevenueTaxed15Percent, x.RevenueTaxed17Percent))
                .Distinct()
                .ToList()
                .ForEach(item.Events.Add);

            var companyEvent = new NewCompanyFromJpkEwpEvent(item.Subject.Name, item.Subject.TaxIdentificationNumber, item.Subject.Address.PostalCode, item.Subject.Address.City, item.Subject.Address.Street, item.Subject.Address.BuildingNumber, item.Subject.Address.LocalNumber, item.Subject.Address.Voivodeship);
            item.Events.Add(companyEvent);

            var result = await base.AddAsync(item);

            return result;
        }
    }
}
