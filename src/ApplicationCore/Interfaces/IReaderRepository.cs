using ApplicationCore.Entities;
using System.Collections.Generic;

namespace ApplicationCore.Interfaces
{
    public interface IReaderRepository
    {
        Reader Add(Reader reader);
        Reader GetById(int id);
        IEnumerable<Reader> GetAll();
        void DeleteBook(Reader reader, Book book);
        void AddBook(Reader reader, Book book);
    }
}