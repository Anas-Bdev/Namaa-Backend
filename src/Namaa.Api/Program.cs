using Namaa.Api;
using Namaa.Application;
using Namaa.Infrastructure;
using Scalar.AspNetCore;
using DotNetEnv;
using Namaa.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Namaa.Application.Features.Identity.Commands.Login;

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

builder.Host.UseSerilog((context, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(context.Configuration)
        .Destructure.ByTransforming<LoginCommand>(cmd => 
            new { cmd.Email, Password = "***", cmd.RememberMe })
            
        .WriteTo.Console();
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();
}
await app.InitializeDatabaseAsync();

app.UseCoreMiddlewares();
if (builder.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}
app.MapControllers();
app.Run();