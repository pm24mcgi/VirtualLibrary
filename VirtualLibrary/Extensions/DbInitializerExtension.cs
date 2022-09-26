using Microsoft.EntityFrameworkCore;
using VL.Shared.Data;
using VL.Shared.Interfaces;

namespace VirtualLibrary.Extensions;

internal static class DbInitializerExtension
{
    public static async Task MigrateDatabasesAsync(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var applicationDbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
        await applicationDbContext!.Database.MigrateAsync();
    }

    public static async Task SeedDataAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var seedingService = services.GetRequiredService<ISeedingService>();
        await seedingService.SeedBooksAsync(500);
    }
}
