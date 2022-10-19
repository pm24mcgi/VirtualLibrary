using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VL.Shared.Interfaces;
using VL.Shared.Model;
using static System.Reflection.Metadata.BlobBuilder;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAnyUser")]
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
        public async Task<ActionResult<BookDto>> GetBooks()
        {
            var bookListDto = await _bookService.GetBooksAsync();
            if (!bookListDto.Any()) return NotFound();
            return Ok(bookListDto);
        }

        /// <summary>
        /// Get a specific book by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Book</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var bookDto = await _bookService.GetBookAsync(id);
            if (bookDto == null) return NotFound();
            return Ok(bookDto);
        }

        /// <summary>
        /// Edit a book
        /// </summary>
        /// <param name="bookDto">book</param>
        /// <returns>Updated book</returns>
        [HttpPut, Authorize("IsLibrarian")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BookDto>> UpdateBook(BookDto bookDto)
        {
            var updateBookDto = await _bookService.UpdateBookAsync(bookDto);
            return Accepted(updateBookDto);
        }

        /// <summary>
        /// Create a new book
        /// </summary>
        /// <param name="bookDto">book</param>
        /// <returns>Newly created book</returns>
        [HttpPost, Authorize("IsLibrarian")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<CreatedResult> CreateBook(BookDto bookDto)
        {
            var newBookDto = await _bookService.CreateBookAsync(bookDto);
            return Created("New Book", newBookDto);
        }

        /// <summary>
        /// Delete a book by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>"Book was successfully deleted"</returns>
        [HttpDelete("{id}"), Authorize("IsLibrarian")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);

            return result ? Ok() : NotFound();
        }
    }
}