using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyDocs.Models;

namespace MyDocs.Infraestructure.Persistence.Configurations
{
    public class AlertConfiguration : IEntityTypeConfiguration<Alert>
    {
        public void Configure(EntityTypeBuilder<Alert> builder)
        {
            builder.HasOne(a => a.User)
                .WithMany(a => a.Alerts)
                .HasForeignKey(a => a.IdUser);
        }
    }
}
