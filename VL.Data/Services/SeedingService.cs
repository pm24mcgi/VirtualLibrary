using Bogus;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VL.Shared.Data;
using VL.Shared.Interfaces;
using VL.Shared.Models;
using IdentityRole = Microsoft.AspNetCore.Identity.IdentityRole;
using IdentityUser = Microsoft.AspNetCore.Identity.IdentityUser;

namespace VL.Shared.Services
{
    public class SeedingService : ISeedingService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly Faker _faker;
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _userManager;
        private readonly Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> _roleManager;
        private readonly Microsoft.AspNetCore.Identity.IUserStore<IdentityUser> _userStore;
        private readonly Microsoft.AspNetCore.Identity.IUserEmailStore<IdentityUser> _emailStore;

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

        public async Task SeedUsersAsync(int count)
        {
            if (await _applicationDbContext.Users.AnyAsync())
            {
                return;
            }

            var user = CreateUser();

            await _userStore.SetUserNameAsync(user, "user@vl.com", CancellationToken.None);
            await _emailStore.SetEmailAsync(user, "user@vl.com", CancellationToken.None);
            await _userManager.CreateAsync(user, "Password@123");
        }
        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        //public async Task SeedRolesAsync(int count)
        //{
        //    _applicationDbContext.Add(new IdentityUserRole<string>
        //    {
        //        RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
        //        UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
        //    });

        //    await _applicationDbContext.SaveChangesAsync();
        //}
    }
}