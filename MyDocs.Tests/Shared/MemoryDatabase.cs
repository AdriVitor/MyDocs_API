using Microsoft.EntityFrameworkCore;
using MyDocs.Infraestructure.Persistence;

namespace MyDocs.Tests.Shared
{
    public static class MemoryDatabase
    {
        public static Context? Create()
        {
            string dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<Context>()
                            .UseInMemoryDatabase(dbName)
                            .Options;

            var context = new Context(options);
            context.Database.EnsureCreated();

            return context;
        }
    }
}
