using MediatR;

namespace PodkarpackiLekarz.Application.Users.Patients.Register;
public class RegisterPatientCommand : RegisterUserCommandBase, IRequest<Guid>
{
    public DateTime DateOfBirth { get; private set; }
    public string Pesel { get; private set; }

    public RegisterPatientCommand(
        string firstName,
        string lastName,
        string email,
        string password,
        string passwordConfirmation,
        DateTime dateOfBirth,
        string pesel)
        :base (firstName, lastName, email, password, passwordConfirmation)
    {
        DateOfBirth = dateOfBirth;
        Pesel = pesel;
    }
}
