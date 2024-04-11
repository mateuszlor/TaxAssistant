using TaxAssistant.JPK.Database;
using TaxAssistant.JPK.Shared.DomainEvents;
using TaxAssistant.JPK.Shared.Model.Database.Kpir;

namespace TaxAssistant.JPK.ApplicationLogic.Repository
{
    public class KpirRepository : BaseRepository<Kpir>
    {
        public KpirRepository(DatabaseContext databaseContext)
            :base(databaseContext)
        {
        }

        public override async Task<Kpir> AddAsync(Kpir item)
        {
            var result = await base.AddAsync(item);

            var companies = result
                .Rows
                .Select(x => new NewCompanyEvent(x.CompanyData, x.CompanyAddress))
                .Distinct()
                .ToList();

            // TODO handle new companies

            return result;
        }
    }
}
