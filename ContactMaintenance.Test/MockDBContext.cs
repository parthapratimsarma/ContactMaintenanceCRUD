using Microsoft.EntityFrameworkCore;

namespace ContactMaintenance.Test
{
    public static class MockDBContext
    {
        public static ContactContext GetMockContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ContactContext>()
                 .UseInMemoryDatabase(databaseName: dbName)
                 .Options;

            var dbContext = new ContactContext(options);
            dbContext.Seed();
            return dbContext;

        }
    }
}
