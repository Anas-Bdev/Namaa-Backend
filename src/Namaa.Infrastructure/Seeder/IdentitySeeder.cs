using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Namaa.Infrastructure.Identity;

namespace Namaa.Infrastructure.Seeder;
public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var scope=services.CreateScope();
        var roleManager=scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
        var userManager=scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var roles=new[] {"Admin","User"};
        foreach (var roleName in roles)
{
    if (!await roleManager.RoleExistsAsync(roleName))
    {
        var roleResult = await roleManager.CreateAsync(new AppRole { Name = roleName });

        if (!roleResult.Succeeded)
            throw new Exception(string.Join(", ", roleResult.Errors.Select(e => e.Description)));
    }
}

        var email="test@namaa.com";
        var password="Pass1234!";
        var user=await userManager.FindByEmailAsync(email);
        if(user is null)
        {
            user =new AppUser
            {
                Email=email,
                UserName="tester",
                EmailConfirmed=true
            };
        
        var userResult=await userManager.CreateAsync(user,password);
        if(!userResult.Succeeded)
      throw new Exception(string.Join(", ", userResult.Errors.Select(e => e.Description)));
      await userManager.AddToRoleAsync(user, "User");
        }
    }
    }