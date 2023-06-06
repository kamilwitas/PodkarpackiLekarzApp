using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PodkarpackiLekarz.Application.Behaviours;
using PodkarpackiLekarz.Application.Calendar.Services;
using PodkarpackiLekarz.Application.Users.Common.Initializer;
using PodkarpackiLekarz.Core.Users.Admins;
using PodkarpackiLekarz.Core.Users.Doctors;
using PodkarpackiLekarz.Core.Users.Patients;
using System.Reflection;
using IdentityUser = PodkarpackiLekarz.Core.Users.Base.IdentityUser;

namespace PodkarpackiLekarz.Application;
public static class Extensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(x =>
        {      
            x.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));            
            x.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
            x.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());            
        });

        services.AddTransient<IPasswordHasher<Patient>, PasswordHasher<Patient>>();
        services.AddTransient<IPasswordHasher<Doctor>, PasswordHasher<Doctor>>();
        services.AddTransient<IPasswordHasher<Administrator>, PasswordHasher<Administrator>>();
        services.AddTransient<IPasswordHasher<IdentityUser>, PasswordHasher<IdentityUser>>();
        services.AddTransient<IScheduleCreator, ScheduleCreator>();
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
