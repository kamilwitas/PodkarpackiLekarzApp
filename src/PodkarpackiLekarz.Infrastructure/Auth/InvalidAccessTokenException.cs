using PodkarpackiLekarz.Shared.Exceptions;

namespace PodkarpackiLekarz.Infrastructure.Auth;

public class InvalidAccessTokenException : AuthorizationException
{
    public InvalidAccessTokenException() : base("Invalid access token")
    {
    }
}