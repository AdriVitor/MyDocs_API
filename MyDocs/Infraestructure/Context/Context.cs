using Microsoft.EntityFrameworkCore;
using MyDocs.Models;

namespace MyDocs.Infraestructure.Persistence
{
    public class Context : DbContext
    {
        public Context() { }
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserCredentials> UsersCredentials { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
        }
    }
}
