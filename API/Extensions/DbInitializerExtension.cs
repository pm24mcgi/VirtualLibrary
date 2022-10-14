using Microsoft.EntityFrameworkCore;
using VL.Shared.Data;
using VL.Shared.Interfaces;

namespace API.Extensions;

internal static class DbInitializerExtension
{
    public static async Task MigrateDatabasesAsync(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var applicationDbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
        var services = scope.ServiceProvider;
        var migrateService = services.GetRequiredService<IMigrateService>();
        var result = await migrateService.HasRolesAsync();
        if (!result)
        {
            await applicationDbContext!.Database.MigrateAsync();
        }
    }

    public static async Task SeedDataAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var seedingService = services.GetRequiredService<ISeedingService>();
        await seedingService.SeedBooksAsync(500);
        await seedingService.SeedRolesAsync();
        await seedingService.SeedUsersAsync();
    }
}
