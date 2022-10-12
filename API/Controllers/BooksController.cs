using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VL.Shared.Interfaces;
using VL.Shared.Model;

namespace API.Controllers
{
    [Authorize]
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
        /// GET: api/Books
        /// </summary>
        /// <returns></returns>
        [HttpGet, Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookService.GetBooksAsync();
            if (books == null) return BadRequest();
            return Ok(books);
        }

        /// <summary>
        /// GET: api/Books/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookService.GetBookAsync(id);
            if (book == null) return BadRequest();
            return Ok(book);
        }

        /// <summary>
        /// PUT: api/Books/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateBook(Book book)
        {
            var bookUpdate = await _bookService.UpdateBookAsync(book);
            return Ok(bookUpdate);
        }

        /// <summary>
        /// POST: api/Books
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        //[HttpPost("{id}")]
        //public async Task<ActionResult<Book>> PostBook([FromRoute] int id, [FromBody] Book book)
        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            var newBook = await _bookService.CreateBookAsync(book);
            if (newBook == null) return BadRequest();
            return Ok(newBook);
        }

        /// <summary>
        /// DELETE: api/Books/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}