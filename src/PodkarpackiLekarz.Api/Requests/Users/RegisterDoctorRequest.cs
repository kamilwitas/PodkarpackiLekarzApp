namespace PodkarpackiLekarz.Api.Requests.Users;

public record RegisterDoctorRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string PasswordConfirmation,
    Guid DoctorTypeId,
    string Description,
    string medicalLicenseNumber);


