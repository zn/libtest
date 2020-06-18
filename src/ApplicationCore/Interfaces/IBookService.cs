using ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IBookService
    {
        void AcceptBook(int bookId, int userId);
        void GiveBook(int bookId, int userId);
        Task<IEnumerable<Book>> GetAvailableBooks();
    }
}