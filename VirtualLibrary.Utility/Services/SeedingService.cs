using VL.Data;
using VL.Models;
using VL.Utility.Interfaces;


namespace VL.Utility.Services
{
    public class SeedingService : ISeedingService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public SeedingService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task SeedBooksAsync(int count)
        {
            // I want to see if any books are inside of the databse on startup
            var bookCheck = _applicationDbContext.Book;
            // If there are no books
            if (bookCheck == null)
            {
                return;
                //// Call SeedBooksAsync
                //await SeedBooksAsync(
                //    // Key into the DB
                //    // Call Add
                //    // Create a new book instance
                //    ApplicationDbContext.Add(new Book { Id = 1, Title = "Book1", Author = "Author1", ReleaseDate = new DateTime(2008, 6, 1, 0, 0, 0), CheckedOut = false, Description = "This is a lovely book." });

                //// Save the cahnges to the DB
                //ApplicationDbContext.SaveChanges();
            }
            else
            {
                return;
            }
        }
    }
}
