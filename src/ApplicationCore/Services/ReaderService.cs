using ApplicationCore.Interfaces;
using ApplicationCore.Exceptions;
using ApplicationCore.Entities;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ReaderService : IReaderService
    {
        private readonly IReaderRepository _repository;
        public ReaderService(IReaderRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Book> GetTakenBooks(int userId)
        {
            var reader = _repository.GetById(userId);
            return reader.Books.Select(br => br.Book).ToList();
        }
    }
}