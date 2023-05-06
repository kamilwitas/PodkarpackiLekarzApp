using PodkarpackiLekarz.Shared.Exceptions;

namespace PodkarpackiLekarz.Application.Exceptions.Users;

public class InvalidAccessTokenException : AuthorizationException
{
    public InvalidAccessTokenException()
        : base("Invalid access token")
    {
    }
}
