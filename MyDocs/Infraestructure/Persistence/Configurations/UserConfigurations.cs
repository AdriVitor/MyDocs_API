using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyDocs.Models;

namespace MyDocs.Infraestructure.Persistence.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(u => u.UserCredentials)
                .WithOne(u => u.User)
                .HasForeignKey<UserCredentials>(u => u.IdUser);

            builder.HasMany(u => u.Alerts)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.IdUser);

            builder.HasMany(u => u.Documents)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.IdUser);
        }
    }
}
