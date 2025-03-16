using Microsoft.Extensions.Logging;
using TaxAssistant.DDD.Abstraction;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Database;
using TaxAssistant.JPK.Shared.Model.Domain.Invoice;

namespace TaxAssistant.JPK.ApplicationLogic.Repository
{
    public class InvoiceRepository : BaseRepository<Invoice>
    {
        public InvoiceRepository(DatabaseContext databaseContext, IDomainEventDispatcher dispatcher, ILogger<InvoiceRepository> logger)
            : base(databaseContext, dispatcher, logger)
        {
        }
    }
}
