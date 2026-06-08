using Namaa.Api;
using Namaa.Application;
using Namaa.Infrastructure;
using Scalar.AspNetCore;
using DotNetEnv;
using Namaa.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    Env.TraversePath().Load();
}
builder.Configuration.AddEnvironmentVariables();

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Host.UseSerilog((context, configuration) => 
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();

}
await app.InitializeDatabaseAsync();
app.UseCoreMiddlewares();
app.MapOpenApi();
app.MapScalarApiReference();
app.MapControllers();
app.Run();