using ApplicationCore.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    // При первом запуске программы заполняем её данными
    public class ApplicationContextSeed
    {
        public static async Task SeedAsync(ApplicationContext context, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger<ApplicationContextSeed>();
            await context.Database.EnsureCreatedAsync();

            if (context.Books.Any())
            {
                return;
            }

            context.AddRange(
                new Book
                {
                    Title = "Title #1",
                    Author = "Author #1",
                    TotalAmount = 10,
                    Available = 10,
                    PosterUrl = "https://pub-static.haozhaopian.net/assets/projects/pages/706b5120-accb-11e8-957c-1564ed1e0dc9_33553b99-898d-4c41-bbb2-710e022973d1_thumb.jpg",
                    PublishDate = new DateTime(1929, 02, 02)
                },
                new Book
                {
                    Title = "Title #2",
                    Author = "Author #2",
                    TotalAmount = 1,
                    Available = 1,
                    PosterUrl = "https://i.pinimg.com/originals/58/dc/26/58dc26d70ec2bd146bd7491351d714a9.jpg",
                    PublishDate = new DateTime(1729, 02, 02)
                },
                new Book
                {
                    Title = "Title #3",
                    Author = "Author #3",
                    TotalAmount = 4,
                    Available = 4,
                    PosterUrl = "https://image.posterlounge.com/images/big/1873138.jpg",
                    PublishDate = new DateTime(2009, 02, 02)
                }
            );

            context.Readers.AddRange(
                new Reader { Name = "user #1" },
                new Reader { Name = "user #2" },
                new Reader { Name = "user #3" }
            );

            await context.SaveChangesAsync();
            logger.LogInformation("The database seeded successfully");
        }
    }
}
