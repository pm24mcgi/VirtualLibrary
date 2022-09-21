using VL.Data;
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
            throw new NotImplementedException();
        }
    }
}
