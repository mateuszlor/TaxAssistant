using TaxAssistant.JPK.Database;
using TaxAssistant.JPK.Shared.DomainEvents;
using TaxAssistant.JPK.Shared.Model.Database.Ewp;

namespace TaxAssistant.JPK.ApplicationLogic.Repository
{
    public class EwpRepository : BaseRepository<Ewp>
    {
        public EwpRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public override async Task<Ewp> AddAsync(Ewp item)
        {
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
