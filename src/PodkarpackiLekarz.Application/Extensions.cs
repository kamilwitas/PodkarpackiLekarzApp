using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
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

        return services;
    }
}
