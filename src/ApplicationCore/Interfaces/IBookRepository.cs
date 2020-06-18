using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IBookRepository
    {
        Book GetById(int id);
        Task<List<Book>> GetAllAsync(Func<Book, bool> predicate);
        Book Update(Book book);
    }
}