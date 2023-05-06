namespace PodkarpackiLekarz.Core.Users.Base;
public interface IConfirmableUser
{
    bool CredibilityConfirmed { get; }
    void ConfirmDoctorCredibility();
}
