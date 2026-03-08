using Namaa.Api;
using Namaa.Application;
using Namaa.Infrastructure;
using Namaa.Infrastructure.Settings;
using Scalar.AspNetCore;
using DotNetEnv;
using Namaa.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Namaa.Infrastructure.Seeder;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    Env.TraversePath().Load();
}

builder.Configuration.AddEnvironmentVariables();

builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("Smtp"));

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    await dbContext.Database.MigrateAsync();

    if (app.Environment.IsDevelopment())
    {
        await scope.ServiceProvider.SeedIdentityAsync();
    }
}

app.UseCoreMiddlewares();
app.MapOpenApi();
app.MapScalarApiReference();
app.MapControllers();

app.Run();