using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VL.Shared.Model;
namespace VL.Shared.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Book> Book { get; set; } = default!;
    }
    //add-migration Initial -context ApplicationDbContext -o Data/Migrations
}
