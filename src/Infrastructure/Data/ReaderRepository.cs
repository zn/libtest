using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Exceptions;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ReaderRepository : IReaderRepository
    {
        private readonly ApplicationContext _context;
        public ReaderRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Reader Add(Reader reader)
        {
            var entry = _context.Readers.Add(reader);
            return entry.Entity;
        }

        public Reader GetById(int id)
        {
            return _context.Readers.Include(r => r.Books)
                                   .ThenInclude(rb => rb.Book)
                                   .SingleOrDefault(r => r.Id == id);
        }

        public IEnumerable<Reader> GetAll()
        {
            return _context.Readers.ToList();
        }

        // Удаляет из бд запись в таблице BookReaders (many2many)
        public void DeleteBook(Reader reader, Book book)
        {
            var bookReader = _context.BookReaders.SingleOrDefault(
                br => br.ReaderId == reader.Id && br.BookId == book.Id);
            if (bookReader == null)
            {
                throw new NotFoundException(nameof(BookReader));
            }
            _context.BookReaders.Remove(bookReader);
            _context.SaveChanges();
        }

        // Добавляет в бд запись в таблице BookReaders (many2many)
        public void AddBook(Reader reader, Book book)
        {
            var bookReader = _context.BookReaders.AsNoTracking().SingleOrDefault(
                br => br.ReaderId == reader.Id && br.BookId == book.Id);
            if (bookReader != null)
            {
                throw new AlreadyHasBookException(book.Title);
            }
            _context.BookReaders.Add(new BookReader
            {
                Book = book,
                BookId = book.Id,
                Reader = reader,
                ReaderId = reader.Id
            });
            _context.SaveChanges();
        }
    }
}