using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyDocs.Models;

namespace MyDocs.Infraestructure.Persistence.Configurations
{
    public class UserCredentialsConfiguration : IEntityTypeConfiguration<UserCredentials>
    {
        public void Configure(EntityTypeBuilder<UserCredentials> builder)
        {
            builder.HasOne(uc => uc.User)
                .WithOne(uc => uc.UserCredentials)
                .HasForeignKey<UserCredentials>(uc => uc.IdUser);
        }
    }
}
