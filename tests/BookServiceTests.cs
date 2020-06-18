using Infrastructure.Data;
using ApplicationCore.Interfaces;
using ApplicationCore.Entities;
using ApplicationCore.Services;
using ApplicationCore.Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tests
{
    public class BookServiceTests
    {
        IBookService bookService;

        IBookRepository bookRepository;
        IReaderRepository readerRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "books")
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
        }


        [Test]
        public void GiveBookTest()
        {
            var user = readerRepository.GetById(1);
            Assert.NotNull(user);
            Assert.Zero(user.Books.Count);
            var book = bookRepository.GetById(1);
            Assert.NotNull(book);
            Assert.Zero(book.Readers.Count);

            int availableBefore = book.Available;

            Assert.DoesNotThrow(() => bookService.GiveBook(book.Id, user.Id));
            Assert.Throws(typeof(AlreadyHasBookException), () => bookService.GiveBook(book.Id, user.Id));
            book = bookRepository.GetById(book.Id);
            Assert.AreEqual(availableBefore - 1, book.Available);
            Assert.AreEqual(1, book.Readers.Count);

            user = readerRepository.GetById(user.Id);
            Assert.AreEqual(1, user.Books.Count);
        }

        /* Этот тест и тест ниже (GetAvailableBooksTest) запускать по отдельности!!! */
        [Test]
        public void AcceptBookTest()
        {
            var user = readerRepository.GetById(1);
            Assert.Zero(user.Books.Count);
            var book = bookRepository.GetById(1);
            Assert.Zero(book.Readers.Count);

            int availableBefore = book.Available;

            bookService.GiveBook(book.Id, user.Id);
            book = bookRepository.GetById(book.Id);
            Assert.AreEqual(user.Id, book.Readers.First().ReaderId);
            Assert.AreEqual(availableBefore - 1, book.Available);

            Assert.DoesNotThrow(() => bookService.AcceptBook(book.Id, user.Id));

            book = bookRepository.GetById(book.Id);
            Assert.AreEqual(availableBefore, book.Available);
            user = readerRepository.GetById(user.Id);
            Assert.Zero(book.Readers.Count);
            Assert.Zero(user.Books.Count);
        }

        /* Этот тест и тест выше (AcceptBookTest) запускать по отдельности!!! */
        [Test]
        public async Task GetAvailableBooksTest()
        {
            var book1 = bookRepository.GetById(1);
            book1.Available -= book1.Available; // 0
            book1 = bookRepository.Update(book1);
            Assert.Zero(book1.Available);

            var availableBooks = await bookService.GetAvailableBooks();
            Assert.AreEqual(1, availableBooks.Count());

            var book2 = bookRepository.GetById(2);
            Assert.AreEqual(book2.Id, availableBooks.First().Id);

            var user = readerRepository.GetById(1);
            Assert.Throws(typeof(NoAvailableBooksException), () => bookService.GiveBook(book1.Id, user.Id));

            book1.Available = 10;
            bookRepository.Update(book1);
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