using ApplicationCore;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b => b.Title)
                    .IsRequired()
                    .HasMaxLength(100);

            builder.Property(b => b.Author)
                    .IsRequired()
                    .HasMaxLength(100);

            builder.HasMany(b => b.Readers);
        }
    }
}