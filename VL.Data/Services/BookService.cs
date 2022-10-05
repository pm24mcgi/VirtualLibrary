using System.Linq;
using VL.Shared.Data;
using VL.Shared.Interfaces;
using VL.Shared.Model;

namespace VL.Shared.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BookService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ApplicationDbContext> ProvideApplicationDbContext()
        {
            return _applicationDbContext;
        }
    }
}
