using Microsoft.EntityFrameworkCore;
using TaxAssistant.JPK.Database;

namespace TaxAssistant.JPK.Server
{
    public static class ServiceInstaller
    {
        public static IApplicationBuilder EnsureDatabaseMigration(this IApplicationBuilder app)
        {
            ILogger? logger = null;

            try
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                    logger = scope.ServiceProvider.GetService<ILogger>();

                    dbContext.Database.Migrate();

                    logger?.LogInformation("Database migrated");
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Problem with database migration");

                throw;
            }

            return app;
        }
    }
}
