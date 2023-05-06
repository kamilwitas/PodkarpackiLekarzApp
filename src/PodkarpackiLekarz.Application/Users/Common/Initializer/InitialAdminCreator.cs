using Microsoft.AspNetCore.Identity;
using PodkarpackiLekarz.Core.Users;
using PodkarpackiLekarz.Core.Users.Admins;

namespace PodkarpackiLekarz.Application.Users.Common.Initializer;

public class InitialAdminCreator : IInitialAdminCreator
{
    private readonly IAdministratorsRepository _administratorsRepository;
    private readonly IPasswordHasher<Administrator> _passwordHasher;    

    public InitialAdminCreator(
        IAdministratorsRepository administratorsRepository,
        IPasswordHasher<Administrator> passwordHasher)
    {
        _administratorsRepository = administratorsRepository;
        _passwordHasher = passwordHasher;        
    }

    public void InitializeSystemAdmin()
    {
        var userId = Guid.Parse("98c0f4bb-73e7-46e6-bb70-8f5bee1d0662");

        var initialAdmin = _administratorsRepository.Get(userId);

        if (initialAdmin is not null)
            return;

        initialAdmin = UsersFactory.CreateInitialAministrator(
            userId,
            "System",
            "Admin",
            "system.admin@pla.com");

        var hashedPassword = _passwordHasher.HashPassword(initialAdmin, "admin");

        initialAdmin.SetPassword(hashedPassword);

        _administratorsRepository.Add(initialAdmin);
        _administratorsRepository.Save();
    }
}
