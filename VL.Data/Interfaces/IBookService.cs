using VL.Shared.Data;
using VL.Shared.Model;

namespace VL.Shared.Interfaces
{
    public interface IBookService
    {
        public Task<ApplicationDbContext> ProvideApplicationDbContext();
    }
}