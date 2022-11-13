using Ajmera.Dtos;
using Ajmera.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ajmera.Controllers
{
    [ApiController]
    [Route("Book")]
    public class BookContoller : Controller
    {
        private readonly IBookService bookService;
        private readonly ILogger logger;

        public BookContoller(IBookService bookService, ILogger<BookContoller> logger)
        {
            this.bookService = bookService;
            this.logger = logger;
        }

        /// <summary>
        /// Create new Book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost("AddBook", Name = nameof(AddBook))]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> AddBook([FromBody] BookPostDto bookPostDto)
        {
            try
            {
                logger.LogInformation($"{nameof(AddBook)} - Initiated");

                var result = await bookService.AddBook(bookPostDto);
                logger.LogInformation($"{nameof(AddBook)} - Completed ");
                return StatusCode((int)HttpStatusCode.Created, result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"{nameof(AddBook)} -  {DateTime.Now}");
                return StatusCode((int)HttpStatusCode.BadRequest, "An error occured while adding book");
            }
        }

        /// <summary>
        /// Delete book from ID
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpDelete("DeleteBookById/BookId", Name = nameof(DeleteBookById))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> DeleteBookById(Guid book)
        {
            try
            {
                logger.LogInformation($"{nameof(GetBookById)} - Initiated");

                var result = await bookService.DeleteBookById(book);
                logger.LogInformation($"{nameof(GetBookById)} - Completed");
                return StatusCode((int)HttpStatusCode.OK, result);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, $"{nameof(GetBookById)}");
                return StatusCode((int)HttpStatusCode.BadRequest, "Book not found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"{nameof(GetBookById)}");
                return StatusCode((int)HttpStatusCode.BadRequest, "An error occured while getting book");
            }
        }

        /// <summary>
        /// Get all books
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllBook", Name = nameof(GetAllBook))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> GetAllBook()
        {
            try
            {
                logger.LogInformation($"{nameof(GetAllBook)} - Initiated");
                var result = await bookService.GetBooks();
                logger.LogInformation($"{nameof(GetAllBook)} - Completed");

                return StatusCode((int)HttpStatusCode.OK, result != null && result.Any() ? result : "No Books are found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"{nameof(GetAllBook)} -  {DateTime.Now}");
                return StatusCode((int)HttpStatusCode.BadRequest, "An error occured while getting book");
            }
        }

        /// <summary>
        /// Get Book from ID
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        [HttpGet("GetBookById/BookId", Name = nameof(GetBookById))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> GetBookById(Guid bookId)
        {
            try
            {
                logger.LogInformation($"{nameof(GetBookById)} - Initiated");
                var result = await bookService.GetBookById(bookId);
                logger.LogInformation($"{nameof(GetBookById)} - Completed");
                return StatusCode((int)HttpStatusCode.OK, result);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, $"{nameof(GetBookById)}");
                return StatusCode((int)HttpStatusCode.BadRequest, "Book not found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"{nameof(GetBookById)}");
                return StatusCode((int)HttpStatusCode.BadRequest, "An error occured while getting book");
            }
        }

        /// <summary>
        /// Update book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPut("UpdateBook", Name = nameof(UpdateBook))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> UpdateBook([FromBody] BookDto book)
        {
            try
            {
                logger.LogInformation($"{nameof(GetBookById)} - Initiated");

                var result = await bookService.UpdateBook(book);
                logger.LogInformation($"{nameof(GetBookById)} - Completed");
                return StatusCode((int)HttpStatusCode.OK, result);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, $"{nameof(GetBookById)}");
                return StatusCode((int)HttpStatusCode.BadRequest, "Book not found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"{nameof(GetBookById)}");
                return StatusCode((int)HttpStatusCode.BadRequest, "An error occured while getting book");
            }
        }
    }
}