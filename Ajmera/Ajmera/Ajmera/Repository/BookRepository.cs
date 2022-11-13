using Ajmera.IRepository;
using Ajmera.Models;
using Microsoft.EntityFrameworkCore;

namespace Ajmera.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly AjmeraContext dbContext;

        public BookRepository(AjmeraContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddBook(Book book)
        {
            await dbContext.AddAsync(book);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateBook(Book book)
        {
            dbContext.Books.Update(book);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteBook(Book book)
        {
            dbContext.Books.Remove(book);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Book> GetBookById(Guid bookId)
        {
            var result = await dbContext.Books.FirstAsync(x => x.BookId == bookId);

            return result;
        }

        public async Task<List<Book>> GetBooks()
        {
            var result = await dbContext.Books.ToListAsync();
            return result;
        }
    }
}