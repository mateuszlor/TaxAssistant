using TaxAssistant.JPK.Database;
using TaxAssistant.JPK.Shared.DomainEvents;
using TaxAssistant.JPK.Shared.Model.Database.Fa;

namespace TaxAssistant.JPK.ApplicationLogic.Repository
{
	public class FaRepository : BaseRepository<Fa>
	{
		public FaRepository(DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public override async Task<Fa> AddAsync(Fa item)
		{
			var result = await base.AddAsync(item);

			var companies = result
				.Invoices
				.Select(x => new NewCompanyEvent(x.Seller.Name, x.Seller.TaxIdentificationNumber, x.Seller.Address))
				.Distinct()
				.ToList();

			companies.AddRange(result
				.Invoices
				.Select(x => new NewCompanyEvent(x.Buyer.Name, x.Buyer.TaxIdentificationNumber, x.Buyer.Address))
				.Distinct());

			// TODO handle new companies

			return result;
		}
	}
}
