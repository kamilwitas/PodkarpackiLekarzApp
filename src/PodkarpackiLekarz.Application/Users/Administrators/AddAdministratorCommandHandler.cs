using MediatR;
using Microsoft.AspNetCore.Identity;
using PodkarpackiLekarz.Application.Exceptions.Users;
using PodkarpackiLekarz.Core.Users;
using PodkarpackiLekarz.Core.Users.Admins;

namespace PodkarpackiLekarz.Application.Users.Administrators;
public class AddAdministratorCommandHandler : IRequestHandler<AddAdministratorCommand, Guid>
{
    private readonly IAdministratorsRepository _administratorsRepository;
    private readonly IIdentityUsersRepository _identityUsersRepository;
    private readonly IPasswordHasher<Administrator> _passwordHasher;

    public AddAdministratorCommandHandler(
        IAdministratorsRepository administratorsRepository,
        IIdentityUsersRepository identityUsersRepository,
        IPasswordHasher<Administrator> passwordHasher)
    {
        _administratorsRepository = administratorsRepository;
        _identityUsersRepository = identityUsersRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(AddAdministratorCommand request, CancellationToken cancellationToken)
    {
        if (await _identityUsersRepository.IsExist(request.Email))
            throw new EmailIsInUseException(request.Email);

        var administrator = UsersFactory.CreateAdministrator(
            request.FirstName,
            request.LastName,
            request.Email);

        var hashedPassword = _passwordHasher.HashPassword(administrator, request.Password);

        administrator.SetPassword(hashedPassword);

        await _administratorsRepository.AddAsync(administrator);
        await _administratorsRepository.SaveAsync();

        return administrator.Id;
    }
}
