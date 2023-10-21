using AutoMapper;
using ManagingLib.DAL.Models;
using ManagingLib.ViewModels;
using MangaingLib.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ManagingLib.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly IGenericRepo<Book> _bGenericRepo;
        private readonly IMapper _mapper;
        private readonly IGenericRepo<Author> _aGenericRepo;
        private readonly IGenericRepo<Genre> _gGenericRepo;
        private readonly IGenericRepo<Error> _eGenericRepo;

        public BookController(IGenericRepo<Book> bGenericRepo,IMapper mapper,IGenericRepo<Author> aGenericRepo,IGenericRepo<Genre> gGenericRepo,IGenericRepo<Error> eGenericRepo)
        {
            _bGenericRepo = bGenericRepo;
            _mapper = mapper;
            _aGenericRepo = aGenericRepo;
            _gGenericRepo = gGenericRepo;
            _eGenericRepo = eGenericRepo;
        }
        public async Task<IActionResult> Index(int? genreId)
        {
            ViewBag.genres=await _gGenericRepo.GetAllAsync();
          

            try
            {
                IEnumerable<Book> books;
                if (genreId is not null)
                {
                    var genre = await _gGenericRepo.GetByIdAsync(genreId.Value);
                    ViewBag.selectedGenre = genre.Name;
                    books =await _bGenericRepo.GetByGenreIdAsync(genreId.Value);
                }
                else
                {
                    books = await _bGenericRepo.GetAllAsync();

                }
                var mappedBook = _mapper.Map<IEnumerable<Book>, IEnumerable<BookViewModel>>(books);
                return View(mappedBook);

            } catch (Exception ex)
            {
           
                var error=new Error() { DateTime = DateTime.Now ,Message=ex.Message,ControllerName= "BookController",ActionName="Index" };
                await _eGenericRepo.Add(error);
                await _eGenericRepo.Save();
                return View(error);

            }
           

        }
        public async Task<IActionResult> Create()
        {
            ViewData["Authors"] = await _aGenericRepo.GetAllAsync();
            ViewData["Genres"] = await _gGenericRepo.GetAllAsync();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookViewModel bookVm)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var mappedBook = _mapper.Map<BookViewModel, Book>(bookVm);
                    await _bGenericRepo.Add(mappedBook);
                    await _bGenericRepo.Save();
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception ex)
            {
                var error = new Error() { DateTime = DateTime.Now, Message = ex.Message, ControllerName = "BookController", ActionName = "Create" };
                await _eGenericRepo.Add(error);
                await _eGenericRepo.Save();

            }
            ViewData["Authors"] = await _aGenericRepo.GetAllAsync();
            ViewData["Genres"] = await _gGenericRepo.GetAllAsync();
            return View(bookVm);
        }
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }
                Book book = await _bGenericRepo.GetByIdAsync(id);
                if (book == null)
                {
                    return NotFound();
                }
                var mappedBook = _mapper.Map<Book, BookViewModel>(book);
                return View(mappedBook);

            }
            catch (Exception ex)
            {
                var error = new Error() { DateTime = DateTime.Now, Message = ex.Message, ControllerName = "BookController", ActionName = "Details" };
                await _eGenericRepo.Add(error);
                await _eGenericRepo.Save();
                return View();

            }


        }
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Authors = await _aGenericRepo.GetAllAsync();
            ViewBag.Genres = await _gGenericRepo.GetAllAsync();
            Book book = await _bGenericRepo.GetByIdAsync(id);
            var mappedBook = _mapper.Map<Book, BookViewModel>(book);
            return View(mappedBook);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, BookViewModel bookVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedBook = _mapper.Map<BookViewModel, Book>(bookVm);
                    _bGenericRepo.Update(mappedBook);
                    await _bGenericRepo.Save();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    var error = new Error() { DateTime = DateTime.Now, Message = ex.Message, ControllerName = "BookController", ActionName = "Edit" };
                    await _eGenericRepo.Add(error);
                    await _eGenericRepo.Save();
                }
            }
           
            return View(bookVm);

        }
        public async Task<IActionResult> Delete(int id)
        {
            Book book = await _bGenericRepo.GetByIdAsync(id);
            var mappedBook = _mapper.Map<Book, BookViewModel>(book);
            if (book == null)
            {
                return BadRequest();
            }
            return View(mappedBook);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, BookViewModel bookVm)
        {
            try
            {
                var mappedBook = _mapper.Map<BookViewModel, Book>(bookVm);
                _bGenericRepo.Delete(mappedBook);
                await _bGenericRepo.Save();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                var mappedBook = _mapper.Map<BookViewModel, Book>(bookVm);
                var error = new Error() { DateTime = DateTime.Now, Message = ex.Message, ControllerName = "BookController", ActionName = "Delete" };
                await _eGenericRepo.Add(error);
                await _eGenericRepo.Save();
                return View(mappedBook);

            }
        }

        //Action for soft delete

        //public async Task<IActionResult> ChangeState(int id)
        //{
        //    Book book = await _bGenericRepo.GetByIdAsync(id);
        //    if (book == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        book.IsDeleted = !book.IsDeleted;
        //        _bGenericRepo.Save();
        //        return Ok();
        //    }

        //}
    }
}
