using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using ManagingLib.DAL.Models;
using ManagingLib.Controllers;
using ManagingLib.ViewModels;
using MangaingLib.BLL.Interfaces;
using MangaingLib.BLL.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagingLib.BLL.Tests.Controller
{
    public class AuthorControllerTest
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepo<Author> _genericRepo;
        private readonly IGenericRepo<Error> _eGenericRepo;

        public AuthorControllerTest()
        {
            _genericRepo= A.Fake<IGenericRepo<Author>>();
            _mapper= A.Fake<IMapper>();
            _eGenericRepo = A.Fake<IGenericRepo<Error>>();

        }
        [Fact]
        public void AuthorController_Index_ReturnAllAuthors()
        {
            //A:Arrange
            var Authors = A.Fake<IEnumerable<Author>>();
            A.CallTo(() => _genericRepo.GetAllAsync()).Returns(Authors);
            var controller = new AuthorController(_mapper,_genericRepo,_eGenericRepo);
            //A:Act
            var result = controller.Index();
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();

        }
        [Fact]
        public void AuthorController_Details_ReturnAuthor()
        {
            //A:Arrange
            var id = 1;
            var author = A.Fake<Author>();
            A.CallTo(() => _genericRepo.GetByIdAsync(id)).Returns(author);
            var controller = new AuthorController(_mapper, _genericRepo, _eGenericRepo);

            //A:Act
            var result = controller.Details(id);
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();

        }
        [Theory]
        [InlineData(null)]
        public void AuthorController_Create_ReturnBool(AuthorViewModel authorVm)
        {
            //A:Arrange
            var author = A.Fake<Author>();
             authorVm= A.Fake<AuthorViewModel>();
            A.CallTo(() => _mapper.Map<AuthorViewModel,Author>(authorVm));
            author.Id = 1;
            author.Name ="test";
            author.BirthDate =DateTime.Now;
            author.Nationality = "any Thing";
            A.CallTo(() => _genericRepo.Add(author));
            var controller = new AuthorController(_mapper, _genericRepo, _eGenericRepo);
            //A:Act

            var result = controller.Create(authorVm);
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();

        }
        [Theory]
        [InlineData(1)]

        public void AuthorController_Delete_ReturnBool(int id)
        {
            //A:Arrange

            var author = A.Fake<Author>();
            author.Id = id;
            A.CallTo(() => _genericRepo.Delete(author));
            var controller = new AuthorController(_mapper, _genericRepo, _eGenericRepo);
            //A:Act
            var result = controller.Delete(id);
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();



        }
        [Theory]
        [InlineData(null, 0)]
        public void AuthorController_Edit_ReturnBool(AuthorViewModel authorVm, int id)
        {
            //A:Arrange
            var author = A.Fake<Author>();
            authorVm = A.Fake<AuthorViewModel>();
            A.CallTo(() => _mapper.Map<AuthorViewModel, Author>(authorVm));

            A.CallTo(() => _genericRepo.Update(author));
            var controller = new AuthorController(_mapper, _genericRepo, _eGenericRepo);
            //A:Act
            var result = controller.Edit(id);
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}
