using Microsoft.EntityFrameworkCore;
using VL.Shared.Data;
using VL.Shared.Interfaces;
using VL.Shared.Model;

namespace VL.Shared.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BookService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            return await _applicationDbContext.Book
                .ToListAsync();
        }

        public async Task<Book?> GetBookAsync(int id)
        {
            return await _applicationDbContext.Book.FindAsync(id);
        }

        public async Task<Book?> UpdateBookAsync(Book book)
        {
            _applicationDbContext.Entry(book).State = EntityState.Modified;
            await _applicationDbContext.SaveChangesAsync();
            return await GetBookAsync(book.Id);
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            _applicationDbContext.Book.Add(book);
            await _applicationDbContext.SaveChangesAsync();
            return book;
        }

        public async Task<int> DeleteBookAsync(int id)
        {
            var book = new Book { Id = id };
            _applicationDbContext.Book.Attach(book);
            _applicationDbContext.Book.Remove(book);
            await _applicationDbContext.SaveChangesAsync();
            return id;
        }
    }
}
