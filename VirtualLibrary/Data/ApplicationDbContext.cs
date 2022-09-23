using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VirtualLibrary.Model;

namespace VirtualLibrary.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Book> Book { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Book1", Author = "Author1", ReleaseDate = new DateTime(2008, 6, 1, 0, 0, 0), CheckedOut = false, Description = "This is a lovely book." },
                new Book { Id = 2, Title = "Book2", Author = "Author2", ReleaseDate = new DateTime(2010, 3, 1, 0, 0, 0), CheckedOut = false, Description = "This is a lovely book." },
                new Book { Id = 3, Title = "Book3", Author = "Author3", ReleaseDate = new DateTime(2008, 6, 1, 0, 0, 0), CheckedOut = false, Description = "This is a lovely book." },
                new Book { Id = 4, Title = "Book4", Author = "Author4", ReleaseDate = new DateTime(2011, 6, 1, 0, 0, 0), CheckedOut = false, Description = "This is a lovely book." },
                new Book { Id = 5, Title = "Book5", Author = "Author5", ReleaseDate = new DateTime(1992, 5, 1, 0, 0, 0), CheckedOut = false, Description = "This is a lovely book." },
                new Book { Id = 6, Title = "Book6", Author = "Author6", ReleaseDate = new DateTime(1964, 6, 1, 0, 0, 0), CheckedOut = false, Description = "This is a lovely book." },
                new Book { Id = 7, Title = "Book7", Author = "Author7", ReleaseDate = new DateTime(2020, 9, 1, 0, 0, 0), CheckedOut = false, Description = "This is a lovely book." },
                new Book { Id = 8, Title = "Book8", Author = "Author8", ReleaseDate = new DateTime(2021, 1, 1, 0, 0, 0), CheckedOut = false, Description = "This is a lovely book." }
            );
        }
    }

}
