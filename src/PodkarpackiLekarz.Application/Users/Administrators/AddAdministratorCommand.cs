using MediatR;

namespace PodkarpackiLekarz.Application.Users.Administrators;
public class AddAdministratorCommand : RegisterUserCommandBase, IRequest<Guid>
{
    public AddAdministratorCommand(
        string firstName,
        string lastName,
        string email,
        string password,
        string passwordConfirmation) 
        : base(
            firstName,
            lastName,
            email,
            password,
            passwordConfirmation)
    {
    }
}
