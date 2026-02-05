using Namaa.Api.ExceptionHandling;

namespace Namaa.Api;
public static class DependencyInjection
{
      public static IServiceCollection AddAppConfiguration(this IServiceCollection services)
    {
      services.ConfigureProblemDetails()
      .AddExceptionHandler<GlobalExceptionHandler>()
      .AddControllers();
       return services;
    }
    public static IServiceCollection ConfigureProblemDetails(this IServiceCollection services)
  {
    services.AddProblemDetails(options =>
      {
        options.CustomizeProblemDetails=(context) =>
        {
          context.ProblemDetails.Instance=$"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
          context.ProblemDetails.Extensions.Add("requestId",context.HttpContext.TraceIdentifier);

        };
      });
      return services;
  }
    public static IApplicationBuilder UseCoreMiddlewares(this WebApplication app,IConfiguration configuration)
    {
    if (app.Environment.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
    }
    else
      app.UseExceptionHandler();
      app.UseAuthentication();
      app.UseAuthorization();
      return app;
    }
}