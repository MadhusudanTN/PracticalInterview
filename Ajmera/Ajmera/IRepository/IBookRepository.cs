using Ajmera.Models;

namespace Ajmera.IRepository
{
    public interface IBookRepository
    {
        Task AddBook(Book book);

        Task UpdateBook(Book book);

        Task DeleteBook(Book book);

        Task<List<Book>> GetBooks();

        Task<Book> GetBookById(Guid bookId);
    }
}