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
            // Seed a book into the DB
            _applicationDbContext.Add(new Book { Title = "Book1", Author = "Author1", ReleaseDate = new DateTime(2008, 6, 1, 0, 0, 0), CheckedOut = false, Description = "This is a lovely book." });

            // Await the add and save the changes to the DB
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
