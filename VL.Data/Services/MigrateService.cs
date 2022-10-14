using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VL.Shared.Data;
using VL.Shared.Interfaces;

namespace VL.Shared.Services
{
    public class MigrateService : IMigrateService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public MigrateService(ApplicationDbContext applicationDbContext,
            RoleManager<IdentityRole> roleManager)
        {
            _applicationDbContext = applicationDbContext;
            _roleManager = roleManager;
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
