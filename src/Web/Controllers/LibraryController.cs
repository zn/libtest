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
    public class LibraryController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IReaderService _readerService;
        private readonly IReaderRepository _readerRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public LibraryController(IBookService bookService,
                                 IReaderService readerService,
                                 IReaderRepository readerRepository,
                                 IBookRepository bookRepository,
                                 IMapper mapper)
        {
            _bookService = bookService;
            _readerService = readerService;
            _readerRepository = readerRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var availableBooks = await _bookService.GetAvailableBooks();
            var viewModels = _mapper.Map<IEnumerable<BookViewModel>>(availableBooks);
            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Take(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }

            var readers = _readerRepository.GetAll();
            var vm = new TakeBookViewModel
            {
                Book = _mapper.Map<BookViewModel>(book),
                Readers = _mapper.Map<IEnumerable<ReaderViewModel>>(readers)
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Take(TakeBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _bookService.GiveBook(model.Book.Id, model.Reader.Id);
                    return RedirectToAction("MyBooks", "Reader", new { id = model.Reader.Id });
                }
                catch (NotFoundException)
                {
                    ModelState.AddModelError("", "Ошибка");
                    return View(model);
                }
                catch (NoAvailableBooksException)
                {
                    ModelState.AddModelError("", "Нельзя взять эту книгу!");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Return(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }

            var readers = _readerRepository.GetAll();
            var vm = new ReturnBookViewModel
            {
                Book = _mapper.Map<BookViewModel>(book),
                Readers = _mapper.Map<IEnumerable<ReaderViewModel>>(readers)
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Return(ReturnBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _bookService.AcceptBook(model.Book.Id, model.Reader.Id);
                    return RedirectToAction("MyBooks", "Reader", new { id = model.Reader.Id });
                }
                catch (NotFoundException)
                {
                    ModelState.AddModelError("", "Ошибка");
                    return View(model);
                }
            }
            return View(model);
        }
    }
}