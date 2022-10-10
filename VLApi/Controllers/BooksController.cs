﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// GET: api/Books
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetBooks()
        {
            var context = await _bookService.ProvideApplicationDbContext();
            var result = await context.Book.ToListAsync();

            return Ok(result);
        }

        /// <summary>
        /// GET: api/Books/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// PUT: api/Books/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        // ****Change to patch****
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            var context = await _bookService.ProvideApplicationDbContext();
            if (id != book.Id)
            {
                return BadRequest();
            }

            context.Entry(book).State = EntityState.Modified;

            var result = await context.SaveChangesAsync();

            return Ok();
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
            var context = await _bookService.ProvideApplicationDbContext();
            context.Book.Add(book);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
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
            var context = await _bookService.ProvideApplicationDbContext();
            //context.Book.Where(id)

            //Book book = new Book() { Id = id };
            //context.Book.Attach(book);
            //context.Book.Remove(book);
            //context.SaveChanges();

            return NoContent();
        }
    }
}