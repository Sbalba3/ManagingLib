using AutoMapper;
using FakeItEasy;
using ManagingLib.DAL.Models;
using ManagingLib.Controllers;
using MangaingLib.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagingLib.ViewModels;
using MangaingLib.BLL.Repository;
using Castle.Core.Logging;

namespace ManagingLib.BLL.Tests.Controller
{
    public class BookControllerTest
    {
        private readonly IGenericRepo<Book> _bGenericRepo;
        private readonly IMapper _mapper;
        private readonly IGenericRepo<Author> _aGenericRepo;
        private readonly IGenericRepo<Genre> _gGenericRepo;
        private readonly IGenericRepo<Error> _eGenericRepo;
   

        public BookControllerTest()
        {
            _bGenericRepo = A.Fake<IGenericRepo<Book>>();
            _mapper = A.Fake<IMapper>();
            _aGenericRepo= A.Fake<IGenericRepo<Author>>();
            _gGenericRepo=A.Fake<IGenericRepo<Genre>>();
            _eGenericRepo=A.Fake<IGenericRepo<Error>>();




        }
        [Theory]
        [InlineData(1)]
        public void BookController_Index_ReturnAllBooks(int? genreId)
        {
            //A:Arrange
            var Books=A.Fake<IEnumerable<Book>>();
            A.CallTo(() => _bGenericRepo.GetAllAsync()).Returns(Books);
            var controller = new BookController(_bGenericRepo, _mapper, _aGenericRepo, _gGenericRepo,_eGenericRepo);
            //A:Act
            var result=controller.Index(genreId);
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();

        }
        [Fact]
        public void BookController_Details_ReturnBook()
        {
            //A:Arrange
            var id = 1;
            var book = A.Fake<Book>();
            A.CallTo(()=>_bGenericRepo.GetByIdAsync(id)).Returns(book);
            var controller = new BookController(_bGenericRepo, _mapper, _aGenericRepo, _gGenericRepo,_eGenericRepo);

            //A:Act
            var result = controller.Details(id);
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();

        }
        [Theory]
        [InlineData(null)]
        public void BookController_Create_ReturnBool(BookViewModel bookVm)
        {
            //A:Arrange
            var book = A.Fake<Book>();
            bookVm=A.Fake<BookViewModel>();
            A.CallTo(() => _mapper.Map<BookViewModel, Book>(bookVm));
            book.Id = 1;
            book.Title = "Hellow";
            book.PublicationYear =DateTime.Now;
            book.ISBN = "12easd";
            book.AuthorId = 1;
            book.GenreId = 1;
            A.CallTo(() => _bGenericRepo.Add(book));
            var controller = new BookController(_bGenericRepo, _mapper, _aGenericRepo, _gGenericRepo,_eGenericRepo);
            //A:Act

            var result = controller.Create(bookVm);
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();

        }
        [Theory]
        [InlineData(1)]

        public void BookController_Delete_ReturnBool(int id)
        {
            //A:Arrange

            var book = A.Fake<Book>();
            book.Id = id;
            A.CallTo(() => _bGenericRepo.Delete(book));
            var controller = new BookController(_bGenericRepo, _mapper, _aGenericRepo, _gGenericRepo,_eGenericRepo);
            //A:Act
            var result = controller.Delete(id);
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();



        }
        [Theory]
        [InlineData(null, 0)]
        public void BookController_Edit_ReturnBool(BookViewModel bookVm,int id)
        {
            var book = A.Fake<Book>();
            bookVm = A.Fake<BookViewModel>();
           var mappedBook=  A.CallTo(() => _mapper.Map<BookViewModel, Book>(bookVm));
            
            A.CallTo(() => _bGenericRepo.Update(book));
            var controller = new BookController(_bGenericRepo, _mapper, _aGenericRepo, _gGenericRepo,_eGenericRepo);
            //A:Act
            var result = controller.Edit(id);
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }




    }
}
