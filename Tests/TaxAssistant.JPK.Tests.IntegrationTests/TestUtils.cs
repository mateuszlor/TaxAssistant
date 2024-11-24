using Microsoft.EntityFrameworkCore;
using TaxAssistant.JPK.Database;

namespace TaxAssistant.JPK.Tests.IntegrationTests
{
    internal static class TestUtils
    {
        public static DatabaseContext MakeInMemoryDatabaseContext()
        {
            return new DatabaseContext(
                    new DbContextOptionsBuilder<DatabaseContext>()
                    .EnableSensitiveDataLogging()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options);
        }
    }
}
