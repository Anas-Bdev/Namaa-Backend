using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Namaa.Domain.Common.Constants;
using Namaa.Infrastructure.Identity;

namespace Namaa.Infrastructure.Persistence.Context;
public class ApplicationDbContextInitializer(RoleManager<AppRole> roleManager)
{
    public async Task TrySeedAsync()
    {
        var roles=new[] {AppRoles.Admin,AppRoles.Expert,AppRoles.Farmer,AppRoles.Trader,AppRoles.Investor};
        foreach(var role in roles)
        {
            if (! await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new AppRole {Name=role});
            }
        }
    }
}
public static class InitializerExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope=app.Services.CreateScope();
        var initializer=scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
        await initializer.TrySeedAsync();

    }
}