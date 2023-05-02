using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PodkarpackiLekarz.Infrastructure.Persistence;
using System.ComponentModel.DataAnnotations;

namespace PodkarpackiLekarz.Infrastructure;
public static class Extensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            var connectionString = configuration["Database:ConnectionString"];
            opt.UseSqlServer(connectionString);
        });

        return services;
    }
}
