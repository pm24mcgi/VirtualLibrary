using AutoMapper;
using AutoMapper.QueryableExtensions;
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
            return await _applicationDbContext.Book
                .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<BookDto?> GetBookAsync(int id)
        {
            var book = await _applicationDbContext.Book.FindAsync(id);
            if (book == null) return null;
            var bookDto = _mapper.Map<BookDto>(book);
            return bookDto;
        }

        public async Task<Book?> UpdateBookAsync(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            _applicationDbContext.Book.Update(book);
            await _applicationDbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Book> CreateBookAsync(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
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
