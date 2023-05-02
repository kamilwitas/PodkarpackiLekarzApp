using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PodkarpackiLekarz.Core.Users;
using PodkarpackiLekarz.Core.Users.Doctors;
using PodkarpackiLekarz.Core.Users.Patients;
using PodkarpackiLekarz.Infrastructure.Persistence;
using PodkarpackiLekarz.Infrastructure.Persistence.Repositories.Users;

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

        services.AddScoped<IPatientsRepository, PatientsRepository>();
        services.AddScoped<IDoctorsRepository, DoctorsRepository>();
        services.AddScoped<IIdentityUsersRepository, IdentityUsersRepository>();
        services.AddScoped<IDoctorTypesRepository, DoctorTypesRepository>();


        return services;
    }

    public static IApplicationBuilder ApplyDbMigrations(
        this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            var dbContext= serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
        }

        return app;
    }
}
