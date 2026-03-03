using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Namaa.Infrastructure.Persistence.Context;
public class ApplicationDbContextInitializer
{
    // public async Task TrySeedAsync
}
public static class InitializerExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope=app.Services.CreateScope();
        var initializer=scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();

    }
}