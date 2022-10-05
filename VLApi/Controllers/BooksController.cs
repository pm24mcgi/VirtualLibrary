using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VL.Shared.Data;
using VL.Shared.Interfaces;
using VL.Shared.Model;

namespace VLApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var context = await _bookService.ProvideApplicationDbContext();
            return await context.Book.ToListAsync();
        }

        // GET: api/Books/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var context = await _bookService.ProvideApplicationDbContext();
            var book = await context.Book.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/{id}
        // ****Change to patch****
        [HttpPut("{id}")]
        public async Task<IActionResult> EditBook(int id, Book book)
        {
            var context = await _bookService.ProvideApplicationDbContext();
            if (id != book.Id)
            {
                return BadRequest();
            }

            context.Entry(book).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Books
        //[HttpPost("{id}")]
        //public async Task<ActionResult<Book>> PostBook([FromRoute] int id, [FromBody] Book book)
        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            var context = await _bookService.ProvideApplicationDbContext();
            context.Book.Add(book);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        // DELETE: api/Books/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var context = await _bookService.ProvideApplicationDbContext();
            var book = await context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            context.Book.Remove(book);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
