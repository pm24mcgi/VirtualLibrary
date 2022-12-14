using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VL.Shared.Data;
using VL.Shared.Model;

namespace VirtualLibrary.Pages.Books
{
    public class StatusModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public StatusModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            else
            {
                Book = book;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);

            if (book != null && book.CheckedOut == false)
            {

                Book = book;
                Book.CheckedOut = true;
                await _context.SaveChangesAsync();
            }
            else
            {
                Book = book;
                Book.CheckedOut = false;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
