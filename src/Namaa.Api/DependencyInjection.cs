namespace Namaa.Api;
public static class DependencyInjection
{
      public static IServiceCollection AddAppConfiguration(this IServiceCollection services)
    {
       services.AddControllers();
       return services;
    }
    public static IApplicationBuilder UseCoreMiddlewares(this IApplicationBuilder app,IConfiguration configuration)
    {
      app.UseAuthentication();
      app.UseAuthorization();
        return app;
    }
}