namespace PodkarpackiLekarz.Application.Users;
public class RegisterUserCommandBase
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string PasswordConfirmation { get; private set; }

    public RegisterUserCommandBase(string firstName,
        string lastName,
        string email,
        string password,
        string passwordConfirmation)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        PasswordConfirmation = passwordConfirmation;
    }
}
