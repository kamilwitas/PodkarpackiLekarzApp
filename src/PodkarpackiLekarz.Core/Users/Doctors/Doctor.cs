using PodkarpackiLekarz.Core.Users.Exceptions;
using PodkarpackiLekarz.Shared.Identity;

namespace PodkarpackiLekarz.Core.Users.Doctors;
public class Doctor : IdentityUser
{
    public DoctorProfile DoctorProfile { get; private set; }
    public bool CredibilityConfirmed { get; private set; }

    private Doctor(
        Guid id,
        string firstName,
        string lastName,
        string email,
        DoctorProfile doctorProfile,
        bool confirmed)
        : base(
            id,
            firstName,
            lastName,
            email,
            Role.Doctor)
    {
        DoctorProfile = doctorProfile;
        CredibilityConfirmed = confirmed;
    }

    public Doctor()
    {

    }

    internal static Doctor Create(
        string firstName,
        string lastName,
        string email,
        DoctorType doctorType,
        string description)
    {
        if (string.IsNullOrEmpty(firstName))
            throw new FieldCannotBeNullOrEmptyException(nameof(firstName));

        if (string.IsNullOrEmpty(lastName))
            throw new FieldCannotBeNullOrEmptyException(nameof(lastName));

        if (string.IsNullOrEmpty(email))
            throw new FieldCannotBeNullOrEmptyException(nameof(email));

        var doctorProfile = DoctorProfile.Create(
            doctorType,
            description);

        return new Doctor(
            Guid.NewGuid(),
            firstName,
            lastName,
            email,
            doctorProfile,
            false);
    }
    
    public void ConfirmDoctorCredibility()
    {
        CredibilityConfirmed = true;
    }
}
