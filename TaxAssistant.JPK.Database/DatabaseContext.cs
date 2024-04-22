using Microsoft.EntityFrameworkCore;
using TaxAssistant.CQRS;
using TaxAssistant.JPK.Shared.Model.Database;
using TaxAssistant.JPK.Shared.Model.Database.Ewp;
using TaxAssistant.JPK.Shared.Model.Database.Kpir;

namespace TaxAssistant.JPK.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ewp>()
                .HasOne(e => e.RowsControlData)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ewp>()
                .HasOne(e => e.FixedAssetsControlData)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
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

        public DbSet<Ewp> Ewp { get; set; }
        public DbSet<EwpRow> EwpRow { get; set; }
        public DbSet<EwpControlData> EwpControlData { get; set; }
        public DbSet<EwpHeader> EwpHeader { get; set; }
        public DbSet<EwpFixedAsset> EwpSummary { get; set; }
        public DbSet<EwpCompany> EwpCompany { get; set; }
        public DbSet<EwpCompanyAddress> EwpCompanyAddress { get; set; }
    }
}