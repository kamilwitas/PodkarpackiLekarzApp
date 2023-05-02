using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PodkarpackiLekarz.Application.Auth;
using PodkarpackiLekarz.Shared.Identity;
using System.Runtime.CompilerServices;
using System.Text;

namespace PodkarpackiLekarz.Infrastructure.Auth;
public static class Extensions
{
    internal static void AddPolicies(
        this AuthorizationOptions options,
        string[] appPermissions)
    {
        foreach (var permission in appPermissions)
        {
            options.AddPolicy(permission, policy =>
            {
                policy.RequireClaim("permissions", permission);
            });
        }
    }

    internal static IServiceCollection EnableAuthorizationWithPermissionPolicies(
        this IServiceCollection services)
    {
        services.AddAuthorizationCore(x => x.AddPolicies(Permissions.AllPermissions));

        return services;
    }

    internal static IServiceCollection EnableJwtAuthentication(this IServiceCollection services,
            IConfiguration configuration)
    {
        var issuer = configuration["jwt:issuer"];
        var audience = configuration["jwt:audience"];
        var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:signingKey"]!));

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = configuration["jwt:issuer"],
                ValidAudience = configuration["jwt:audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:signingKey"]!)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            };
        });
        services.AddHttpContextAccessor();
        services.AddScoped<IApplicationPrincipalService, ApplicationPrincipalService>();

        services.Configure<AuthorizationSettings>(configuration.GetSection("jwt"));
        services.AddScoped<IJwtProvider, JwtProvider>();

        return services;
    }
}
