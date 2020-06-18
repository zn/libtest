using Infrastructure.Data;
using ApplicationCore.Interfaces;
using ApplicationCore.Entities;
using ApplicationCore.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tests
{
    public class ReaderServiceTests
    {
        IBookService bookService;
        IReaderService readerService;
        IBookRepository bookRepository;
        IReaderRepository readerRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "readers")
                .Options;

            var context = new ApplicationContext(options);
            seedBooks(context);
            seedReaders(context);

            bookRepository = new BookRepository(context);
            readerRepository = new ReaderRepository(context);

            bookService = new BookService(
                bookRepository,
                readerRepository
            );
            readerService = new ReaderService(readerRepository);
        }

        [Test]
        public async Task GetMyBooksTest()
        {
            var user = readerRepository.GetById(1);
            var books = await bookService.GetAvailableBooks();
            foreach (var book in books)
            {
                bookService.GiveBook(book.Id, user.Id);
            }
            user = readerRepository.GetById(1);
            Assert.AreEqual(books.Count(), user.Books.Count);

            bookService.AcceptBook(1, user.Id);
            books = readerService.GetTakenBooks(user.Id);
            Assert.AreEqual(1, books.Count());
        }


        private void seedBooks(ApplicationContext context)
        {
            var books = new List<Book>
            {
                new Book
                {
                    Title = "War and peace",
                    Author = "Leo Tolstoy",
                    TotalAmount = 10,
                    Available = 10
                },
                new Book
                {
                    Title = "1984",
                    Author = "George Orwell",
                    TotalAmount = 5,
                    Available = 5
                }
            };

            context.Books.AddRange(books);
            context.SaveChanges();
        }

        private void seedReaders(ApplicationContext context)
        {
            var readers = new List<Reader>
            {
                new Reader{Name = "Alex"},
                new Reader{Name = "Ann"},
                new Reader{Name = "John"}
            };
            context.Readers.AddRange(readers);
            context.SaveChanges();
        }
    }
}