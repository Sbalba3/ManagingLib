using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using ManagingLib.DAL.Models;
using Managinglibrary.Controllers;
using ManagingLib.ViewModels;
using MangaingLib.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagingLib.BLL.Tests.Controller
{
    public class GenreControllerTest
    {

        private readonly IMapper _mapper;
        private readonly IGenericRepo<Genre> _genericRepo;
        private readonly IGenericRepo<Error> _eGenericRepo;

        public GenreControllerTest()
        {
            _genericRepo = A.Fake<IGenericRepo<Genre>>();
            _mapper = A.Fake<IMapper>();
            _eGenericRepo = A.Fake<IGenericRepo<Error>>();

        }
        [Fact]
        public void GenreController_Index_ReturnAllGenre()
        {
            //A:Arrange
            var genres = A.Fake<IEnumerable<Genre>>();
            A.CallTo(() => _genericRepo.GetAllAsync()).Returns(genres);
            var controller = new GenreController(_mapper, _genericRepo, _eGenericRepo);
            //A:Act
            var result = controller.Index();
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();

        }
        [Fact]
        public void GenreController_Details_ReturnGenre()
        {
            //A:Arrange
            var id = 1;
            var genre = A.Fake<Genre>();
            A.CallTo(() => _genericRepo.GetByIdAsync(id)).Returns(genre);
            var controller = new GenreController(_mapper, _genericRepo, _eGenericRepo);

            //A:Act
            var result = controller.Details(id);
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();

        }
        [Theory]
        [InlineData(null)]
        public void AuthorController_Create_ReturnBool(GenreViewModel genreVm)
        {
            //A:Arrange
            var genre = A.Fake<Genre>();
            genreVm = A.Fake<GenreViewModel>();
            A.CallTo(() => _mapper.Map<GenreViewModel, Genre>(genreVm));
            genre.Id = 1;
            genre.Name = "testat";
            genre.Description = "How ARE You";
            A.CallTo(() => _genericRepo.Add(genre));
            var controller = new GenreController(_mapper, _genericRepo, _eGenericRepo);
            //A:Act

            var result = controller.Create(genreVm);
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();

        }
        [Theory]
        [InlineData(1)]

        public void AuthorController_Delete_ReturnBool(int id)
        {
            //A:Arrange

            var genre = A.Fake<Genre>();
            genre.Id = id;
            A.CallTo(() => _genericRepo.Delete(genre));
            var controller = new GenreController(_mapper, _genericRepo, _eGenericRepo);
            //A:Act
            var result = controller.Delete(id);
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();



        }
        [Theory]
        [InlineData(null, 0)]
        public void AuthorController_Edit_ReturnBool(GenreViewModel genreVm, int id)
        {
            //A:Arrange
            var genre = A.Fake<Genre>();
            genreVm = A.Fake<GenreViewModel>();
            A.CallTo(() => _mapper.Map<GenreViewModel, Genre>(genreVm));

            A.CallTo(() => _genericRepo.Update(genre));
            var controller = new GenreController(_mapper, _genericRepo, _eGenericRepo);
            //A:Act
            var result = controller.Edit(id);
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}
