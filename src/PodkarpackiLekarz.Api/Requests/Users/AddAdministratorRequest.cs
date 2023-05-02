namespace PodkarpackiLekarz.Api.Requests.Users;

public record AddAdministratorRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string PasswordConfirmation);
