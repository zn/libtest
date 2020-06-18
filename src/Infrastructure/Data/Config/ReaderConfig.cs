using ApplicationCore;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    class ReaderConfig : IEntityTypeConfiguration<Reader>
    {
        public void Configure(EntityTypeBuilder<Reader> builder)
        {
            builder.Property(r => r.Name)
                    .IsRequired()
                    .HasMaxLength(100);

            builder.HasMany(b => b.Books);
        }
    }
}