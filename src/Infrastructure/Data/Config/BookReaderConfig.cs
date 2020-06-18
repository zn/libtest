using ApplicationCore;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    class BookReaderConfig : IEntityTypeConfiguration<BookReader>
    {
        public void Configure(EntityTypeBuilder<BookReader> builder)
        {
            builder.HasKey(br => new { br.BookId, br.ReaderId });
        }
    }
}