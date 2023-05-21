using PodkarpackiLekarz.Core.Users.Doctors;

namespace PodkarpackiLekarz.Core.Users.Base;
public interface IConfirmableUser
{
    public CredibilityConfirmationStatus CredibilityConfirmationStatus { get; }
    void ConfirmDoctorCredibility();
}
