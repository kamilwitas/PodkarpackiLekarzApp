namespace PodkarpackiLekarz.Api.Requests.Users;

public record RegisterPatientRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string PasswordConfirmation,
    DateTime BirthDate,
    string Pesel);


