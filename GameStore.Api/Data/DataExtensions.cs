using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var databaseContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        await databaseContext.Database.MigrateAsync();
    }
}