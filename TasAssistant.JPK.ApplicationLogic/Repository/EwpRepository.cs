using Microsoft.Extensions.Logging;
using TaxAssistant.DDD.Abstraction;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Database;
using TaxAssistant.JPK.Shared.Model.Database.Ewp;
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

            var result = await base.AddAsync(item);

            return result;
        }
    }
}
