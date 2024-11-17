using Microsoft.Extensions.Logging;
using TaxAssistant.DDD.Abstraction;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Database;
using TaxAssistant.JPK.Shared.DomainEvents;
using TaxAssistant.JPK.Shared.Model.Database.Ewp;

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
			//var companies = item
			//	.Rows
			//	.Select(x => new NewCompanyEvent(x.Seller.Name, x.Seller.TaxIdentificationNumber, x.Seller.Address))
			//	.Distinct()
			//	.ToList();

			//companies.AddRange(item
			//	.Invoices
			//	.Select(x => new NewCompanyEvent(x.Buyer.Name, x.Buyer.TaxIdentificationNumber, x.Buyer.Address))
			//	.Distinct());

			var result = await base.AddAsync(item);

            var fixedAssets = result
                .FixedAssets?
                .Select(x => new NewFixedAssetEvent(x.CategoryCode, x.Description, x.DocumentNumber, x.TransferDate, x.AcceptanceDate, x.InitialValue, x.UpdatedInitialValue))
                .Distinct()
                .ToList();

            // TODO handle new fixed assets

            return result;
        }
    }
}
