using AutoMapper;
using ManagingLib.DAL.Models;
using ManagingLib.ViewModels;
using MangaingLib.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagingLib.Controllers
{
    [Authorize]
    public class AuthorController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IGenericRepo<Author> _genericRepo;
        private readonly IGenericRepo<Error> _eGenericRepo;


        public AuthorController( IMapper mapper, IGenericRepo<Author> genericRepo, IGenericRepo<Error> eGenericRepo)
        {
            
            _mapper = mapper;
            _genericRepo = genericRepo;
            _eGenericRepo = eGenericRepo;
          
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Author> authors = await _genericRepo.GetAllAsync();
                var mappedAuthor = _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorViewModel>>(authors);
                return View(mappedAuthor);

            }
            catch (Exception ex)
            {
                var error = new Error() { DateTime = DateTime.Now, Message = ex.Message, ControllerName = "GenreController", ActionName = "Index" };
                await _eGenericRepo.Add(error);
                await _eGenericRepo.Save();
            }
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorViewModel authorVm)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var mappedAuthor = _mapper.Map<AuthorViewModel, Author>(authorVm);
                    await _genericRepo.Add(mappedAuthor);
                    await _genericRepo.Save();
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception ex)
            {
                var error = new Error() { DateTime = DateTime.Now, Message = ex.Message, ControllerName = "GenreController", ActionName = "Create" };
                await _eGenericRepo.Add(error);
                await _eGenericRepo.Save();
            }
         
            return View(authorVm);
        }
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }
                Author author = await _genericRepo.GetByIdAsync(id);
                if (author == null)
                {
                    return NotFound();
                }
                var mappedAuthor = _mapper.Map<Author, AuthorViewModel>(author);
                return View(mappedAuthor);

            }
            catch (Exception ex)
            {
                var error = new Error() { DateTime = DateTime.Now, Message = ex.Message, ControllerName = "GenreController", ActionName = "Details" };
                await _eGenericRepo.Add(error);
                await _eGenericRepo.Save();
            }
            return View();
          
        }
        public async Task<IActionResult> Edit(int id)
        {
          
            Author author = await _genericRepo.GetByIdAsync(id);
            var mappedAuthor = _mapper.Map<Author, AuthorViewModel>(author);
            return View(mappedAuthor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, AuthorViewModel authorVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedauthor = _mapper.Map<AuthorViewModel, Author>(authorVm);
                    _genericRepo.Update(mappedauthor);
                    await _genericRepo.Save();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    var error = new Error() { DateTime = DateTime.Now, Message = ex.Message, ControllerName = "GenreController", ActionName = "Edit" };
                    await _eGenericRepo.Add(error);
                    await _eGenericRepo.Save();
                }
            }
            return View(authorVm);

        }
        public async Task<IActionResult> Delete(int id)
        {
            Author author   = await _genericRepo.GetByIdAsync(id);
            var mappedauthor = _mapper.Map<Author, AuthorViewModel>(author);
            if (author == null)
            {
                return BadRequest();
            }
            return View(mappedauthor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, AuthorViewModel authorVm)
        {
            try
            {
                var mappedauthor = _mapper.Map<AuthorViewModel, Author>(authorVm);
                _genericRepo.Delete(mappedauthor);
                await _genericRepo.Save();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                var mappedAuthor = _mapper.Map<AuthorViewModel, Author>(authorVm);
                var error = new Error() { DateTime = DateTime.Now, Message = ex.Message, ControllerName = "GenreController", ActionName = "Delete" };
                await _eGenericRepo.Add(error);
                await _eGenericRepo.Save();
                return View(mappedAuthor);

            }
        }
    }
}
