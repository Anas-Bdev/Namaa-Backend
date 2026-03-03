using Namaa.Api;
using Namaa.Application;
using Namaa.Infrastructure;
using Namaa.Infrastructure.Seeder;
using Namaa.Infrastructure.Settings;
using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("Smtp"));
builder.Services.AddPresentation()
.AddApplication().AddInfrastructure(builder.Configuration);
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseCoreMiddlewares();
app.MapControllers();
app.Run();
