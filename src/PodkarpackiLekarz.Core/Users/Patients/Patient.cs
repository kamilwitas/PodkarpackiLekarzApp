using PodkarpackiLekarz.Core.Users.Exceptions;
using PodkarpackiLekarz.Shared.Identity;

namespace PodkarpackiLekarz.Core.Users.Patients;
public class Patient : IdentityUser
{    
    public DateTime DateOfBirth { get; private set; }
    public string Pesel { get; private set; }

    public Patient() : base(){ }

    private Patient(
        Guid id,
        string firstName,
        string lastName,
        string email,
        DateTime birthDate,
        string pesel) : base(
            id,
            firstName,
            lastName,
            email,
            Role.Patient)
    {
        DateOfBirth = birthDate;
        Pesel = pesel;
    }

    internal static Patient Create(
        string firstName,
        string lastName,
        string email,
        DateTime birthDate,
        string pesel)
    {
        if (string.IsNullOrEmpty(firstName))
            throw new FieldCannotBeNullOrEmptyException(nameof(firstName));

        if (string.IsNullOrEmpty(lastName))
            throw new FieldCannotBeNullOrEmptyException(nameof(lastName));

        if (string.IsNullOrEmpty(email))
            throw new FieldCannotBeNullOrEmptyException(nameof(email));

        if (string.IsNullOrEmpty(pesel))
            throw new FieldAccessException(nameof(pesel));

        return new Patient(
            Guid.NewGuid(),
            firstName,
            lastName,
            email,
            birthDate,
            pesel);
    }
}
