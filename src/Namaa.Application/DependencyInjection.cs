using System.Data;
using System.Runtime.CompilerServices;
using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Namaa.Application.Common.Behaviors;

namespace Namaa.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly=Assembly.GetExecutingAssembly();
        services.AddValidatorsFromAssembly(assembly);
        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssembly(assembly);
            conf.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        return services;
    }
}