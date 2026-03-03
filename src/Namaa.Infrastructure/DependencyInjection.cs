
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Namaa.Infrastructure.Identity;
using Namaa.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Namaa.Application.Common.Interfaces;
using Namaa.Infrastructure.Services;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Namaa.Infrastructure.Persistence.Interceptors;

namespace Namaa.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(TimeProvider.System);
        services.AddHttpContextAccessor();

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        ArgumentNullException.ThrowIfNull(connectionString);
        
        services.AddScoped<ApplicationDbContextInitializer>();
        
        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            var interceptor = sp.GetRequiredService<ISaveChangesInterceptor>();
            options.UseSqlServer(connectionString)
                   .AddInterceptors(interceptor);
        });

        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

        services.AddIdentityCore<AppUser>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = true;
            options.Password.RequiredUniqueChars = 1;
            options.Password.RequiredLength = 8;
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        })
        .AddRoles<AppRole>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddSignInManager()
        .AddDefaultTokenProviders();

        var jwtSettings = configuration.GetSection("JwtSettings");
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateActor = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]!))
            };
        });

        services.Configure<DataProtectionTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromHours(3);
        });

        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<ITokenProvider, TokenProvider>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IEmailTemplateService, EmailTemplateService>();

        return services;
    }
}