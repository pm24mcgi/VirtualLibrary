using VL.Shared.Model;

namespace VL.Shared.Interfaces
{
    public interface IBookService
    {
        public Task<List<Book>> GetBooksAsync();

        public Task<Book?> GetBookAsync(int id);

        public Task<Book?> UpdateBookAsync(Book book);

        public Task<Book> CreateBookAsync(Book book);

        public Task<bool> DeleteBookAsync(int id);
    }
}