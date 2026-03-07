using Namaa.Api;
using Namaa.Application;
using Namaa.Infrastructure;
using Namaa.Infrastructure.Settings;
using Scalar.AspNetCore;
using DotNetEnv;
using Namaa.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
var envPath=Path.Combine(Directory.GetCurrentDirectory(), "../../.env");
if (File.Exists(envPath))
{
    Env.Load(envPath);
}
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("Smtp"));
builder.Services.AddPresentation()
.AddApplication().AddInfrastructure(builder.Configuration);
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
 using var serviceScope=app.Services.CreateScope();
 using var dbContext=serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
 dbContext?.Database.Migrate();
app.UseCoreMiddlewares();
app.MapControllers();
app.Run();
