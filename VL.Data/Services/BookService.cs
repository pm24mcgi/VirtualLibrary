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
            var bookUpdate = new Book();
            {
                Id = book.Id;
                bookUpdate.Title = book.Title;
                bookUpdate.Description = book.Description;
                bookUpdate.Author = book.Author;
            }

            _applicationDbContext.Book.Update(bookUpdate);
            await _applicationDbContext.SaveChangesAsync();
            return bookUpdate;
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            _applicationDbContext.Book.Add(book);
            await _applicationDbContext.SaveChangesAsync();
            return book;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            _applicationDbContext.Book.Remove(new Book
            {
                Id = id
            });
            var result = await _applicationDbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
