using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PodkarpackiLekarz.Application.Users.Common.Initializer;
using PodkarpackiLekarz.Core.Users;
using PodkarpackiLekarz.Core.Users.Admins;
using PodkarpackiLekarz.Core.Users.Doctors;
using PodkarpackiLekarz.Core.Users.Patients;

namespace PodkarpackiLekarz.Application;
public static class Extensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        });

        services.AddTransient<IPasswordHasher<Patient>, PasswordHasher<Patient>>();
        services.AddTransient<IPasswordHasher<Doctor>, PasswordHasher<Doctor>>();
        services.AddTransient<IPasswordHasher<Administrator>, PasswordHasher<Administrator>>();
        services.AddTransient<IPasswordHasher<IdentityUser>, PasswordHasher<IdentityUser>>();
        services.AddScoped<IInitialAdminCreator, InitialAdminCreator>();

        return services;
    }

    public static IApplicationBuilder UseApplication(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var initialAdminCreator = scope.ServiceProvider.GetService<IInitialAdminCreator>();
            initialAdminCreator!.InitializeSystemAdmin();
        }

        return app;
    }
}
