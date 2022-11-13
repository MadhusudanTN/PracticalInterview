using Ajmera.Dtos;
using Ajmera.IRepository;
using Ajmera.IServices;
using Ajmera.Models;
using AutoMapper;

namespace Ajmera.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public BookService(IBookRepository bookRepository, ILogger<BookService> logger, IMapper mapper)
        {
            this.bookRepository = bookRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<Guid> AddBook(BookPostDto bookPostDto)
        {
            var bookDto = new BookDto(bookPostDto);
            var entity = mapper.Map<Book>(bookDto);
            entity.BookId = entity.BookId == Guid.Empty ? Guid.NewGuid() : bookDto.BookId;
            entity.CreateTs = DateTime.Now;
            await bookRepository.AddBook(entity);
            logger.LogInformation($"Service-{nameof(AddBook)}-Executing completed");
            return entity.BookId;
        }

        public async Task<BookDto> UpdateBook(BookDto book)
        {
            var dbBook = await bookRepository.GetBookById(book.BookId);
            if (dbBook == null)
                throw new InvalidOperationException("Book Not Found");

            dbBook.AuthorName = book.AuthorName;
            dbBook.BookName = book.Name;
            dbBook.UpdateTs = DateTime.Now;

            await bookRepository.UpdateBook(dbBook);
            logger.LogInformation($"Service-{nameof(UpdateBook)}-Executing completed");
            return mapper.Map<BookDto>(dbBook);
        }

        public async Task<BookDto> DeleteBookById(Guid bookId)
        {
            var book = await bookRepository.GetBookById(bookId);
            if (book == null)
                throw new InvalidOperationException("Book Not Found");

            await bookRepository.DeleteBook(book);
            logger.LogInformation($"Service-{nameof(DeleteBookById)}-Executing completed");
            return mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> GetBookById(Guid bookId)
        {
            var result = await bookRepository.GetBookById(bookId);
            if (result == null)
                throw new InvalidOperationException("Book Not Found");
            logger.LogInformation($"Service-{nameof(GetBookById)}-Executing completed");
            return mapper.Map<BookDto>(result);
        }

        public async Task<List<BookDto>> GetBooks()
        {
            var result = await bookRepository.GetBooks();
            logger.LogInformation($"Service-{nameof(GetBooks)}-Executing completed");
            return mapper.Map<List<BookDto>>(result);
        }
    }
}