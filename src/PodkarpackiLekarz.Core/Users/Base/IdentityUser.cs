using PodkarpackiLekarz.Core.Users.Exceptions;
using PodkarpackiLekarz.Shared.Identity;

namespace PodkarpackiLekarz.Core.Users.Base;
public class IdentityUser
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public Role Role { get; private set; }
    public UserSession? Session { get; private set; }

    public IdentityUser(
        Guid id,
        string firstName,
        string lastName,
        string email,
        Role role)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Role = role;
    }

    public IdentityUser()
    {

    }

    public void SetPassword(string password)
    {
        Password = password;
    }

    public void ValidatePassword(string password)
    {
        if (password != Password)
            throw new IncorrectPasswordException();
    }

    public void SetSession(
            string refreshToken,
            DateTime refreshTokenExpirationDate)
    {
        Session = UserSession
            .SetSession(refreshToken, refreshTokenExpirationDate);
    }

    public void RevokeSession()
    {
        Session?.Revoke();
    }
}
