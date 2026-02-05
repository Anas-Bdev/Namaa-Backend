using Namaa.Api;
using Namaa.Infrastructure;
using Namaa.Infrastructure.Seeder;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAppConfiguration();
builder.Services.AddInfrastructure(builder.Configuration);
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    await app.Services.SeedIdentityAsync();
}
app.UseCoreMiddlewares(builder.Configuration);
app.MapControllers();
app.Run();
