using AutoMapper;
using ManagingLib.DAL.Models;
using ManagingLib.ViewModels;
using MangaingLib.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Managinglibrary.Controllers
{
    [Authorize]
    public class GenreController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepo<Genre> _genericRepo;
        private readonly IGenericRepo<Error> _eGenericRepo;

        public GenreController(IMapper mapper, IGenericRepo<Genre> genericRepo, IGenericRepo<Error> eGenericRepo)
        {

            _mapper = mapper;
            _genericRepo = genericRepo;
            _eGenericRepo = eGenericRepo;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Genre> genres = await _genericRepo.GetAllAsync();
                var mappedGenre = _mapper.Map<IEnumerable<Genre>, IEnumerable<GenreViewModel>>(genres);
                return View(mappedGenre);

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
        public async Task<IActionResult> Create(GenreViewModel genreVm)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var mappedGenre = _mapper.Map<GenreViewModel, Genre>(genreVm);
                    await _genericRepo.Add(mappedGenre);
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

            return View(genreVm);
        }
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }
                Genre genre = await _genericRepo.GetByIdAsync(id);
                if (genre == null)
                {
                    return NotFound();
                }
                var mappedGenre = _mapper.Map<Genre, GenreViewModel>(genre);
                return View(mappedGenre);

            }
            catch (Exception ex)
            {
                var error = new Error() { DateTime = DateTime.Now, Message = ex.Message, ControllerName = "GenreController", ActionName = "Create" };
                await _eGenericRepo.Add(error);
                await _eGenericRepo.Save();
            }
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {

            Genre genre = await _genericRepo.GetByIdAsync(id);
            var mappedGenre = _mapper.Map<Genre, GenreViewModel>(genre);
            return View(mappedGenre);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, GenreViewModel genreVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedGenre = _mapper.Map<GenreViewModel, Genre>(genreVm);
                    _genericRepo.Update(mappedGenre);
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
            return View(genreVm);

        }
        public async Task<IActionResult> Delete(int id)
        {
            Genre genre = await _genericRepo.GetByIdAsync(id);
            var mappedGenre = _mapper.Map<Genre, GenreViewModel>(genre);
            if (genre == null)
            {
                return BadRequest();
            }
            return View(mappedGenre);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, GenreViewModel genreVm)
        {
            try
            {
                var mappedGenre = _mapper.Map<GenreViewModel, Genre>(genreVm);
                _genericRepo.Delete(mappedGenre);
                await _genericRepo.Save();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                var mappedAuthor = _mapper.Map<GenreViewModel, Genre>(genreVm);
                var error = new Error() { DateTime = DateTime.Now, Message = ex.Message, ControllerName = "GenreController", ActionName = "Delete" };
                await _eGenericRepo.Add(error);
                await _eGenericRepo.Save();
                return View(mappedAuthor);

            }
        }
    }
}
