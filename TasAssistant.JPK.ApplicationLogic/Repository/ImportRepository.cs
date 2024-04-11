using TaxAssistant.JPK.Database;
using TaxAssistant.JPK.Shared.Model.Database;

namespace TaxAssistant.JPK.ApplicationLogic.Repository
{
    public class ImportRepository : BaseRepository<Import>
    {
        public ImportRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
    }
}
