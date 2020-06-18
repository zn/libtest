using ApplicationCore.Interfaces;
using ApplicationCore.Exceptions;
using ApplicationCore.Entities;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IReaderRepository _readerRepository;
        public BookService(IBookRepository bookRepository, IReaderRepository readerRepository)
        {
            _bookRepository = bookRepository;
            _readerRepository = readerRepository;
        }

        public void AcceptBook(int bookId, int userId)
        {
            var book = _bookRepository.GetById(bookId);
            if (book == null)
            {
                throw new NotFoundException(nameof(Book));
            }

            var user = _readerRepository.GetById(userId);
            if (user == null)
            {
                throw new NotFoundException(nameof(Reader));
            }

            _readerRepository.DeleteBook(user, book);
            book.Available++;
            _bookRepository.Update(book);
        }

        public void GiveBook(int bookId, int userId)
        {
            var book = _bookRepository.GetById(bookId);
            if (book == null)
            {
                throw new NotFoundException(nameof(Book));
            }

            if (book.Available < 1)
            {
                throw new NoAvailableBooksException(book.Title);
            }

            var user = _readerRepository.GetById(userId);
            if (user == null)
            {
                throw new NotFoundException(nameof(Reader));
            }

            _readerRepository.AddBook(user, book);
            book.Available--;
            _bookRepository.Update(book);
        }

        public async Task<IEnumerable<Book>> GetAvailableBooks()
        {
            return await _bookRepository.GetAllAsync(b => b.Available > 0);
        }
    }
}