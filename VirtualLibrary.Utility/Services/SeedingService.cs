using Bogus;
using Microsoft.EntityFrameworkCore;
using VL.Data.Data;
using VL.Models;
using VL.Utility.Interfaces;

namespace VL.Utility.Services
{
    public class SeedingService : ISeedingService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly Faker _faker;

        public SeedingService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _faker = new Faker
            {
                Random = new Randomizer(8675309)
            };
        }

        public async Task SeedBooksAsync(int count)
        {
            if (await _applicationDbContext.Book.AnyAsync())
            {
                return;
            }

            var books = new Faker<Book>()
                .RuleFor(u => u.Title, (f, u) => f.Commerce.ProductName())
                .RuleFor(u => u.Author, (f, u) => f.Person.FullName)
                .RuleFor(u => u.ReleaseDate, f => f.Date.Past(_faker.Random.Number(1, 10)))
                .RuleFor(u => u.CheckedOut, (f, u) => f.Random.Bool())
                .RuleFor(u => u.Description, (f, u) => f.Commerce.ProductDescription())
                .Generate(count);

            await _applicationDbContext.AddRangeAsync(books);

            await _applicationDbContext.SaveChangesAsync();
        }
    }
}