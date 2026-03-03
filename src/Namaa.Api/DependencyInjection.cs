using System.Text.Json.Serialization;
using Namaa.Api.ExceptionHandling;
using Namaa.Api.OpenApi.Transformer;
using Namaa.Api.Services;
using Namaa.Application.Common.Interfaces;

namespace Namaa.Api;
public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
      services.AddControllerWithJsonConfiguration()
            .ConfigureProblemDetails()
            .AddApiDocumentation()
            .AddIdentityInfrastructure()
            .AddExceptionHandling();

        return services;
    }

    private static IServiceCollection ConfigureProblemDetails(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = (context) =>
            {
                context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
                context.ProblemDetails.Extensions.Add("requestId", context.HttpContext.TraceIdentifier);
            };
        });
        return services;
    }

    private static IServiceCollection AddExceptionHandling(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        return services;
    }
    private static IServiceCollection AddControllerWithJsonConfiguration(this IServiceCollection services)
  {
    services.AddControllers().AddJsonOptions(options =>
    {
      options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
      options.JsonSerializerOptions.DefaultIgnoreCondition=JsonIgnoreCondition.WhenWritingNull;
    });
    return services;
  }
    private static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IUser, CurrentUser>();
        return services;
    }

    private static IServiceCollection AddApiDocumentation(this IServiceCollection services)
    {
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
            options.AddOperationTransformer<BearerSecuritySchemeTransformer>();
        });
        return services;
    }

    public static IApplicationBuilder UseCoreMiddlewares(this WebApplication app)
    {
        app.UseExceptionHandler();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}