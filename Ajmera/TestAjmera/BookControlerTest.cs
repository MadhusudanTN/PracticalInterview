using Ajmera.Controllers;
using Ajmera.Dtos;
using Ajmera.Helper;
using Ajmera.IRepository;
using Ajmera.Models;
using Ajmera.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TestAjmera
{
    public class BookControlerTest
    {
        private readonly IMapper mapper;

        public BookControlerTest()
        {
            mapper = new MapperConfiguration(o =>
            {
                o.AddProfile(new AjemraMapper());
            }).CreateMapper();
        }

        [Fact]
        public async Task GetBooks_ShouldReturnStatus200()
        {
            var mockRepo = new Mock<IBookRepository>();
            mockRepo.Setup(x => x.GetBooks())
                .Returns(Task.FromResult(new List<Book>
                {
                     new Book{AuthorName ="madhu",BookName =  "Book1",BookId = Guid.NewGuid(),CreateTs  = DateTime.Now},
                     new Book{AuthorName ="madhu",BookName =  "Book2",BookId = Guid.NewGuid(),CreateTs  = DateTime.Now.AddDays(4)},
                }));
            var service = new BookService(mockRepo.Object, Substitute.For<ILogger<BookService>>(), mapper);
            var controller = new BookContoller(service, Substitute.For<ILogger<BookContoller>>());
            ObjectResult reponse = (ObjectResult)await controller.GetAllBook();

            var responseObject = (List<BookDto>)reponse.Value;
            Assert.Equal(200, reponse.StatusCode);
            mockRepo.Verify(x => x.GetBooks());
            Assert.Equal(2, responseObject.Count);
            Assert.Equal("Book1", responseObject[0].Name);
        }

        [Fact]
        public async Task GetBooks_ShouldReturnStatus200WithNoBooks()
        {
            var mockRepo = new Mock<IBookRepository>();
            mockRepo.Setup(x => x.GetBooks())
                .Returns(Task.FromResult(new List<Book>()));
            var service = new BookService(mockRepo.Object, Substitute.For<ILogger<BookService>>(), mapper);
            var controller = new BookContoller(service, Substitute.For<ILogger<BookContoller>>());
            ObjectResult reponse = (ObjectResult)await controller.GetAllBook();

            var responseObject = reponse.Value;
            Assert.Equal(200, reponse.StatusCode);
            mockRepo.Verify(x => x.GetBooks());
            Assert.Equal("No Books are found", responseObject);
        }

        [Fact]
        public async Task AddBook_ShouldReturnStatus200_2()
        {
            var mockRepo = new Mock<IBookRepository>();

            var service = new BookService(mockRepo.Object, Substitute.For<ILogger<BookService>>(), mapper);
            var controller = new BookContoller(service, Substitute.For<ILogger<BookContoller>>());

            ObjectResult reponse = (ObjectResult)await controller.AddBook(new BookDto { Name = "Book1" });

            mockRepo.Verify(x => x.AddBook(It.IsAny<Book>()));

            Assert.Equal(201, reponse.StatusCode);
        }

        [Fact]
        public async Task AddBook_ShouldNotReturnStatus201()
        {
            var mockRepo = new Mock<IBookRepository>();
            var service = new BookService(mockRepo.Object, Substitute.For<ILogger<BookService>>(), mapper);
            var controller = new BookContoller(service, Substitute.For<ILogger<BookContoller>>());

            ObjectResult reponse = (ObjectResult)await controller.AddBook(new BookDto { Name = "Book1", AuthorName = "Madhu" });

            mockRepo.Verify(x => x.AddBook(It.IsAny<Book>()));

            Assert.Equal(201, reponse.StatusCode);
        }

        [Fact]
        public async Task GetBookById_ShouldReturnStatus200()
        {
            var mockRepo = new Mock<IBookRepository>();
            Guid guid = Guid.NewGuid();
            mockRepo.Setup(x => x.GetBookById(guid))
              .ReturnsAsync(new Book { AuthorName = "madhu", BookId = guid, BookName = "Book1" });
            var service = new BookService(mockRepo.Object, Substitute.For<ILogger<BookService>>(), mapper);
            var controller = new BookContoller(service, Substitute.For<ILogger<BookContoller>>());

            ObjectResult reponse = (ObjectResult)await controller.GetBookById(guid);

            var responseObject = reponse.Value;

            Assert.Equal(200, reponse.StatusCode);
            Assert.Equal(guid, (responseObject as BookDto).BookId);
        }

        [Fact]
        public async Task GetBookById_ShouldReturnStatus400()
        {
            var mockRepo = new Mock<IBookRepository>();
            Guid guid = Guid.NewGuid();
            mockRepo.Setup(x => x.GetBookById(guid))
              .ReturnsAsync(new Book { AuthorName = "madhu", BookId = guid, BookName = "Book1" });
            var service = new BookService(mockRepo.Object, Substitute.For<ILogger<BookService>>(), mapper);
            var controller = new BookContoller(service, Substitute.For<ILogger<BookContoller>>());

            ObjectResult reponse = (ObjectResult)await controller.GetBookById(Guid.NewGuid());

            var responseObject = reponse.Value;

            Assert.Equal(400, reponse.StatusCode);
            Assert.Equal("Book not found", responseObject);
        }

        [Fact]
        public async Task UpdateBook_ShouldReturnStatus200()
        {
            var mockRepo = new Mock<IBookRepository>();
            Guid guid = Guid.NewGuid();
            var bookdto = new BookDto() { BookId = guid, AuthorName = "madhu", Name = "Book3" };

            mockRepo.Setup(x => x.GetBookById(guid))
              .ReturnsAsync(new Book { AuthorName = "madhu", BookId = guid, BookName = "Book1" });

            var service = new BookService(mockRepo.Object, Substitute.For<ILogger<BookService>>(), mapper);
            var controller = new BookContoller(service, Substitute.For<ILogger<BookContoller>>());

            ObjectResult reponse = (ObjectResult)await controller.UpdateBook(bookdto);

            var responseObject = (BookDto)reponse.Value;

            Assert.Equal(200, reponse.StatusCode);
            Assert.Equal("Book3", responseObject.Name);
        }

        [Fact]
        public async Task UpdateBook_ShouldReturnStatus400()
        {
            var mockRepo = new Mock<IBookRepository>();
            Guid guid = Guid.NewGuid();
            var bookdto = new BookDto() { BookId = guid, AuthorName = "madhu", Name = "Book3" };

            var service = new BookService(mockRepo.Object, Substitute.For<ILogger<BookService>>(), mapper);
            var controller = new BookContoller(service, Substitute.For<ILogger<BookContoller>>());

            ObjectResult reponse = (ObjectResult)await controller.UpdateBook(bookdto);

            var responseObject = reponse.Value;

            Assert.Equal(400, reponse.StatusCode);
            Assert.Equal("Book not found", responseObject);
        }

        [Fact]
        public async Task DeleteBookById_ShouldReturnStatus200()
        {
            var mockRepo = new Mock<IBookRepository>();
            Guid guid = Guid.NewGuid();
            mockRepo.Setup(x => x.GetBookById(guid))
            .ReturnsAsync(new Book { AuthorName = "madhu", BookId = guid, BookName = "Book1" });

            var service = new BookService(mockRepo.Object, Substitute.For<ILogger<BookService>>(), mapper);
            var controller = new BookContoller(service, Substitute.For<ILogger<BookContoller>>());

            ObjectResult reponse = (ObjectResult)await controller.DeleteBookById(guid);

            var responseObject = (BookDto)reponse.Value;

            Assert.Equal(200, reponse.StatusCode);
            Assert.Equal("Book1", responseObject.Name);
        }

        [Fact]
        public async Task DeleteBookById_ShouldReturnStatus400()
        {
            var mockRepo = new Mock<IBookRepository>();
            Guid guid = Guid.NewGuid();
            var service = new BookService(mockRepo.Object, Substitute.For<ILogger<BookService>>(), mapper);
            var controller = new BookContoller(service, Substitute.For<ILogger<BookContoller>>());

            ObjectResult reponse = (ObjectResult)await controller.DeleteBookById(guid);

            var responseObject = reponse.Value;

            Assert.Equal(400, reponse.StatusCode);
            Assert.Equal("Book not found", responseObject);
        }
    }
}