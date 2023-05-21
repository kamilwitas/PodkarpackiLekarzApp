using PodkarpackiLekarz.Core.Users.Doctors;

namespace PodkarpackiLekarz.Application.Dtos.Users;
public class DoctorBasicDto
{
    private CredibilityConfirmationStatus _status;
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public string CredibilityConfirmationStatus
    {
        get => _status.ToString();
        set => _status = Enum.TryParse<CredibilityConfirmationStatus>(value, out var result)
            ? result
            : Core.Users.Doctors.CredibilityConfirmationStatus.Unknown;
    }
}
