using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyDocs.Models;

namespace MyDocs.Infraestructure.Persistence.Configurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.HasOne(d => d.User)
                .WithMany(d => d.Documents)
                .HasForeignKey(d => d.IdUser);
        }
    }
}
