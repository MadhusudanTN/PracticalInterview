using Ajmera.Dtos;

namespace Ajmera.IServices
{
    public interface IBookService
    {
        Task<Guid> AddBook(BookPostDto bookPostDto);

        Task<BookDto> DeleteBookById(Guid bookId);

        Task<BookDto> UpdateBook(BookDto book);

        Task<List<BookDto>> GetBooks();

        Task<BookDto> GetBookById(Guid bookId);
    }
}