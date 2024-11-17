using Microsoft.Extensions.Logging;
using TaxAssistant.DDD.Abstraction;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Database;
using TaxAssistant.JPK.Shared.Model.Domain.Company;

namespace TaxAssistant.JPK.ApplicationLogic.Repository
{
	public class CompanyRepository : BaseRepository<Company>
	{
		public CompanyRepository(DatabaseContext databaseContext, IDomainEventDispatcher dispatcher, ILogger<CompanyRepository> logger)
			: base(databaseContext, dispatcher, logger)
		{
		}
	}
}
