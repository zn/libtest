using System;
using System.Collections.Generic;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationContext _context;
        public BookRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllAsync(Func<Book, bool> predicate = null)
        {
            if (predicate == null)
            {
                return await _context.Books.Include(b => b.Readers).AsNoTracking().ToListAsync();
            }
            return await Task.FromResult(
                _context.Books.Include(b => b.Readers)
                                .AsNoTracking()
                                .Where(predicate)
                                .ToList());
        }

        public Book GetById(int id)
        {
            return _context.Books.Include(b => b.Readers)
                                 .ThenInclude(br => br.Reader)
                                 .FirstOrDefault(b => b.Id == id);
        }

        public Book Update(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            _context.SaveChanges();
            return book;
        }
    }
}