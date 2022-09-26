using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VL.Shared.Data;
using VL.Shared.Interfaces;
using VL.Shared.Model;

namespace VL.Shared.Services
{
    public class SeedingService : ISeedingService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly Faker _faker;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedingService(
            ApplicationDbContext applicationDbContext,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _applicationDbContext = applicationDbContext;
            _faker = new Faker
            {
                Random = new Randomizer(8675309)
            };
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedBooksAsync(int count)
        {
            if (await _applicationDbContext.Book.AnyAsync())
            {
                return;
            }

            Randomizer.Seed = new Random(1338);
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

        public async Task SeedRolesAsync()
        {
            if (await _applicationDbContext.Roles.AnyAsync())
            {
                return;
            }

            await _roleManager.CreateAsync(new IdentityRole(Roles.Librarian));
            await _roleManager.CreateAsync(new IdentityRole(Roles.User));
        }

        public async Task SeedUsersAsync()
        {
            if (await _applicationDbContext.Users.AnyAsync())
            {
                return;
            }

            var user = new IdentityUser
            {
                UserName = "user@vl.com",
                Email = "user@vl.com"
            };

            var userResult = await _userManager.CreateAsync(user, "Password@123");

            if (userResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.User);
            }

            var librarian = new IdentityUser
            {
                UserName = "librarian@vl.com",
                Email = "librarian@vl.com"
            };

            var librarianResult = await _userManager.CreateAsync(librarian, "Password@123");

            if (librarianResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(librarian, Roles.Librarian);
            }
        }
    }
}