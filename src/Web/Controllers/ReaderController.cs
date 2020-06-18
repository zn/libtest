using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;
using Web.ViewModels;
using ApplicationCore.Exceptions;

namespace Web.Controllers
{
    public class ReaderController : Controller
    {
        private readonly IReaderService _readerService;
        private readonly IReaderRepository _readerRepository;
        private readonly IMapper _mapper;
        public ReaderController(IReaderService readerService,
                                 IReaderRepository readerRepository,
                                 IMapper mapper)
        {
            _readerService = readerService;
            _readerRepository = readerRepository;
            _mapper = mapper;
        }

        public IActionResult MyBooks(int id)
        {
            var reader = _readerRepository.GetById(id);
            if (reader == null)
            {
                return NotFound();
            }
            var takenBooks = _readerService.GetTakenBooks(id);
            return View(_mapper.Map<IEnumerable<BookViewModel>>(takenBooks));
        }
    }
}