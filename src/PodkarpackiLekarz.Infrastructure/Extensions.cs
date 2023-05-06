﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PodkarpackiLekarz.Core.Users;
using PodkarpackiLekarz.Core.Users.Admins;
using PodkarpackiLekarz.Core.Users.Doctors;
using PodkarpackiLekarz.Core.Users.Patients;
using PodkarpackiLekarz.Infrastructure.Auth;
using PodkarpackiLekarz.Infrastructure.Exceptions;
using PodkarpackiLekarz.Infrastructure.Persistence;
using PodkarpackiLekarz.Infrastructure.Persistence.Repositories.Users;
using PodkarpackiLekarz.Shared.Identity;

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
        services.AddScoped<IAdministratorsRepository, AdministratorsRepository>();

        services.AddTransient<GlobalExceptionHandlerMiddleware>();

        services.EnableAuthorizationWithPermissionPolicies();

        services.EnableAuthorizationWithPermissionPolicies();
        services.EnableJwtAuthentication(configuration);


        return services;
    }

    public static IApplicationBuilder UseInfrastructure(
        this IApplicationBuilder app)
    {
        MigrateDatabase(app);

        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

        return app;
    }

    private static void MigrateDatabase(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
        }
    }    
}
