using Microsoft.Extensions.Logging;
using TaxAssistant.DDD.Abstraction;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Database;
using TaxAssistant.JPK.Shared.Model.Database;

namespace TaxAssistant.JPK.ApplicationLogic.Repository
{
	public class ImportRepository : BaseRepository<Import>
    {
        public ImportRepository(DatabaseContext databaseContext, IDomainEventDispatcher dispatcher, ILogger<ImportRepository> logger)
			: base(databaseContext, dispatcher, logger)
		{
        }
    }
}
