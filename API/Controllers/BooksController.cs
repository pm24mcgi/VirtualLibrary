using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VL.Shared.Interfaces;
using VL.Shared.Model;
using static System.Reflection.Metadata.BlobBuilder;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAllUsers")]
    [Route("api/vl")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Get all books
        /// </summary>
        /// <returns>A list of all books</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookService.GetBooksAsync();
            if (books.Count == 0) return NotFound();
            return Ok(books);
        }

        /// <summary>
        /// Get a specific book by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Book</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookService.GetBookAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        /// <summary>
        /// Edit a book
        /// </summary>
        /// <param name="book">book</param>
        /// <returns>Updated book</returns>
        [HttpPut("{id}"), Authorize("IsLibrarian")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]

        public async Task<IActionResult> UpdateBook(Book book)
        {
            var bookUpdate = await _bookService.UpdateBookAsync(book);
            return Accepted(bookUpdate);
        }

        /// <summary>
        /// Create a new book
        /// </summary>
        /// <param name="book">book</param>
        /// <returns>Newly created book</returns>
        [HttpPost, Authorize("IsLibrarian")]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public async Task<CreatedResult> CreateBook(Book book)
        {
            var newBook = await _bookService.CreateBookAsync(book);
            return Created("New Book", newBook);
        }

        /// <summary>
        /// Delete a book by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>"Book was successfully deleted"</returns>
        [HttpDelete("{id}"), Authorize("IsLibrarian")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);

            if (result)
            {
                return Accepted("Book was successfully deleted");
            }
            return NotFound();
        }
    }
}