using MediatR;
using PodkarpackiLekarz.Application.Auth;

namespace PodkarpackiLekarz.Application.Users.Common.SignIn;
public class SignInCommand : IRequest
{
    public string Email { get; private set; }
    public string Password { get; private set; }

    public SignInCommand(
        string email,
        string password)
    {
        Email = email;
        Password = password;
    }
}
