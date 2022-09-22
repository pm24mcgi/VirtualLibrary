using VL.Utility.Interfaces;

namespace VirtualLibrary.Extensions;

internal static class DbInitializerExtension
{
    public static async Task UseItToSeedSqlServerAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var seedingService = services.GetRequiredService<ISeedingService>();
        await seedingService.SeedBooksAsync(500);
    }
}
