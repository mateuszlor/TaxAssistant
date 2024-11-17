using Microsoft.Extensions.Logging;
using TaxAssistant.DDD.Abstraction;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Database;
using TaxAssistant.JPK.Shared.DomainEvents;
using TaxAssistant.JPK.Shared.Model.Database.Kpir;

namespace TaxAssistant.JPK.ApplicationLogic.Repository
{
	public class KpirRepository : BaseRepository<Kpir>
    {
        public KpirRepository(DatabaseContext databaseContext, IDomainEventDispatcher dispatcher, ILogger<KpirRepository> logger)
			: base(databaseContext, dispatcher, logger)
		{
        }

        public override async Task<Kpir> AddAsync(Kpir item)
		{
			var companies = item
				.Rows
				.Select(x => new NewCompanyEvent(x.CompanyData, x.CompanyAddress))
				.Distinct()
				.ToList();

            companies.ForEach(x => item.Events.Add(x));

			var result = await base.AddAsync(item);

            // TODO handle new companies

            return result;
        }
    }
}
