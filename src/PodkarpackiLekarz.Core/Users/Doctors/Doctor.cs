using PodkarpackiLekarz.Core.Users.Base;
using PodkarpackiLekarz.Core.Users.Exceptions;
using PodkarpackiLekarz.Shared.Identity;

namespace PodkarpackiLekarz.Core.Users.Doctors;
public class Doctor : IdentityUser, IConfirmableUser
{
    public DoctorProfile DoctorProfile { get; private set; }
    public CredibilityConfirmationStatus CredibilityConfirmationStatus { get; private set; }

    private Doctor(
        Guid id,
        string firstName,
        string lastName,
        string email,
        DoctorProfile doctorProfile,
        CredibilityConfirmationStatus credibilityConfirmationStatus)
        : base(
            id,
            firstName,
            lastName,
            email,
            Role.Doctor)
    {
        DoctorProfile = doctorProfile;
        CredibilityConfirmationStatus = credibilityConfirmationStatus;
    }

    public Doctor()
    {

    }

    internal static Doctor Create(
        string firstName,
        string lastName,
        string email,
        DoctorType doctorType,
        string description,
        string medicalLicenseNumber)
    {
        if (string.IsNullOrEmpty(firstName))
            throw new FieldCannotBeNullOrEmptyException(nameof(firstName));

        if (string.IsNullOrEmpty(lastName))
            throw new FieldCannotBeNullOrEmptyException(nameof(lastName));

        if (string.IsNullOrEmpty(email))
            throw new FieldCannotBeNullOrEmptyException(nameof(email));

        var doctorProfile = DoctorProfile.Create(
            doctorType,
            description,
            medicalLicenseNumber);

        return new Doctor(
            Guid.NewGuid(),
            firstName: firstName,
            lastName: lastName,
            email: email,
            doctorProfile: doctorProfile,
            CredibilityConfirmationStatus.Waiting);
    }

    public void ConfirmDoctorCredibility()
    => CredibilityConfirmationStatus = CredibilityConfirmationStatus.Confirmed;
    public void RejectDoctorCredibility()
        => CredibilityConfirmationStatus = CredibilityConfirmationStatus.Rejected;
}
