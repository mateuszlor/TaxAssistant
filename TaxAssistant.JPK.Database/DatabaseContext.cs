using Microsoft.EntityFrameworkCore;
using TaxAssistant.JPK.Shared.Model.Database;
using TaxAssistant.JPK.Shared.Model.Database.Kpir;

namespace TaxAssistant.JPK.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Import> Import { get; set; }
        public DbSet<Kpir> Kpir { get; set; }
        public DbSet<KpirRow> KpirRow { get; set; }
        public DbSet<KpirControlData> KpirControlData { get; set; }
        public DbSet<KpirHeader> KpirHeader { get; set; }
        public DbSet<KpirSummary> KpirSummary { get; set; }
        public DbSet<KpirPhysicalInventory> KpirPhisicalInventory { get; set; }
        public DbSet<KpirCompany> KpirCompany { get; set; }
        public DbSet<KpirCompanyAddress> KpirComapnyAddress { get; set; }
    }
}