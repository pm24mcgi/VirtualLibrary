using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VL.Shared.Data;
using VL.Shared.Interfaces;
using VL.Shared.Model;

namespace VL.Shared.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public BookService(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetBooksAsync()
        {
            var bookList = await _applicationDbContext.Book
                .ToListAsync();
            var mappedBookList = bookList.Select(bookList => _mapper.Map<BookDto>(bookList));
            return mappedBookList;
        }

        public async Task<BookDto?> GetBookAsync(int id)
        {
            var book = await _applicationDbContext.Book.FindAsync(id);
            if (book == null) return null;
            var mappedBook = _mapper.Map<BookDto>(book);
            return mappedBook;
        }

        public async Task<Book?> UpdateBookAsync(BookDto bookDto)
        {
            var mappedBook = _mapper.Map<Book>(bookDto);
            _applicationDbContext.Book.Update(mappedBook);
            await _applicationDbContext.SaveChangesAsync();
            return mappedBook;
        }

        public async Task<Book> CreateBookAsync(BookDto bookDto)
        {
            var mappedBook = _mapper.Map<Book>(bookDto);
            _applicationDbContext.Book.Add(mappedBook);
            await _applicationDbContext.SaveChangesAsync();
            return mappedBook;
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
