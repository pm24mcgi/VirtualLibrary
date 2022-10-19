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

        public async Task<List<Book>> GetBooksAsync()
        {
            return await _applicationDbContext.Book
                .ToListAsync();
        }

        public async Task<Book?> GetBookAsync(int id)
        {
            return await _applicationDbContext.Book.FindAsync(id);
        }

        public async Task<Book?> UpdateBookAsync(UpdateBookDto book)
        {
            var mappedBook = _mapper.Map<Book>(book);
            _applicationDbContext.Book.Update(mappedBook);
            await _applicationDbContext.SaveChangesAsync();
            return mappedBook;
        }

        public async Task<Book> CreateBookAsync(UpdateBookDto book)
        {
            var mappedBook = _mapper.Map<Book>(book);
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
