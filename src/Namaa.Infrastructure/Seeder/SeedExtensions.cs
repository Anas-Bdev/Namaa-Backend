namespace Namaa.Infrastructure.Seeder;
public static class SeedExtensions
{
    public static async Task SeedIdentityAsync(this IServiceProvider services) => await IdentitySeeder.SeedAsync(services);
}