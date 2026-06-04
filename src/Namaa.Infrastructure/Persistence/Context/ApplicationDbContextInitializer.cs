using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Namaa.Domain.Common.Constants;
using Namaa.Infrastructure.Identity;

namespace Namaa.Infrastructure.Persistence.Context;
public class ApplicationDbContextInitializer(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
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

        var adminEmail="admin@namaa.com";
        var adminPassword="Admin@123456";
        if(await userManager.FindByEmailAsync(adminEmail) is null)
        {
            var adminUser= new AppUser
            {
                UserName=adminEmail,
                Email=adminEmail,
               EmailConfirmed=true,
               FirstName="Admin",
               LastName="Namaa",
            };

            var result=await userManager.CreateAsync(adminUser,adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser,AppRoles.Admin);
            }

            else
            {
                var errors=string.Join(",", result.Errors.Select(e => e.Description));
                Console.WriteLine($"Failed to create admin user: {errors}");
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