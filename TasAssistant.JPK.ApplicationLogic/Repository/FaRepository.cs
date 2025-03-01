using Microsoft.Extensions.Logging;
using TaxAssistant.DDD.Abstraction;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Database;
using TaxAssistant.JPK.Shared.Model.Database.Fa;
using TaxAssistant.JPK.Shared.Model.Domain.Company.Events;

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
                ?.Select(x => new NewCompanyEvent(x.Seller.Name, x.Seller.TaxIdentificationNumber, x.Seller.Address))
                .Distinct()
                .ToList()
                ?? [];

            events.AddRange(item
                .Invoices
                ?.Select(x => new NewCompanyEvent(x.Buyer.Name, x.Buyer.TaxIdentificationNumber, x.Buyer.Address))
                .Distinct() ?? []);

            events.ForEach(x => item.Events.Add(x));

            var result = await base.AddAsync(item);

            return result;
        }
    }
}
