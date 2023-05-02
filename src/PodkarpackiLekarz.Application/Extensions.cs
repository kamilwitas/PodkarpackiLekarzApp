using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PodkarpackiLekarz.Core.Users.Doctors;
using PodkarpackiLekarz.Core.Users.Patients;
using System.Reflection;

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

        return services;
    }
}
