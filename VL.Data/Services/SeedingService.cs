using Bogus;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VL.Shared.Data;
using VL.Shared.Interfaces;
using VL.Shared.Models;
using IdentityRole = Microsoft.AspNetCore.Identity.IdentityRole;
using IdentityUser = Microsoft.AspNetCore.Identity.IdentityUser;
using System.Data.Entity;

namespace VL.Shared.Services
{
    public class SeedingService : ISeedingService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly Faker _faker;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;

        //public SeedingService(
        //    UserManager<IdentityUser> userManager,
        //    RoleManager<IdentityRole> roleManager,
        //    IUserStore<IdentityUser> userStore)
        //{
        //    _userManager = userManager;
        //    _roleManager = roleManager;
        //    _userStore = userStore;
        //    _emailStore = GetEmailStore();
        //}

        public SeedingService(ApplicationDbContext applicationDbContext, RoleManager<IdentityRole> roleManager, IUserStore<IdentityUser> userStore, UserManager<IdentityUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _faker = new Faker
            {
                Random = new Randomizer(8675309)
            };
            _roleManager = roleManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _userManager = userManager;
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

        public async Task SeedRolesAsync(int count)
        {

            if (await _applicationDbContext.Roles.AnyAsync())
            {
                return;
            }

            await _roleManager.CreateAsync(new IdentityRole(Roles.Librarian));
            await _roleManager.CreateAsync(new IdentityRole(Roles.User));
        }

        public async Task SeedUsersAndRolesAsync(int count)
        {
            if (await _applicationDbContext.Users.AnyAsync())
            {
                return;
            }

            var user = CreateUser();

            await _userStore.SetUserNameAsync(user, "user@vl.com", CancellationToken.None);
            await _emailStore.SetEmailAsync(user, "user@vl.com", CancellationToken.None);
            await _userManager.CreateAsync(user, "Password@123");
            await _userManager.AddToRoleAsync(user, Roles.User);

            await _userStore.SetUserNameAsync(user, "librarian@vl.com", CancellationToken.None);
            await _emailStore.SetEmailAsync(user, "librarian@vl.com", CancellationToken.None);
            await _userManager.CreateAsync(user, "Password@123");
            await _userManager.AddToRoleAsync(user, Roles.Librarian);
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
        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}