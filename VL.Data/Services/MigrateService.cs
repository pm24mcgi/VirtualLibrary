using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VL.Shared.Data;
using VL.Shared.Interfaces;

namespace VL.Shared.Services
{
    public class MigrateService : IMigrateService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public MigrateService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> HasRolesAsync()
        {
            if (await _applicationDbContext.Roles.AnyAsync())
            {
                return true;
            }
            return false;
        }
    }
}
