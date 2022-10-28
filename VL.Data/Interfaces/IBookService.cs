using VL.Shared.Model;

namespace VL.Shared.Interfaces
{
    public interface IBookService
    {
        public Task<IEnumerable<BookDto>> GetBooksAsync();

        public Task<BookDto?> GetBookAsync(int id);

        public Task<Book?> UpdateBookAsync(BookDto book);

        public Task<Book> CreateBookAsync(BookDto book);

        public Task<bool> DeleteBookAsync(int id);
    }
}