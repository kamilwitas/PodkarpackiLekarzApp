using PodkarpackiLekarz.Shared.Exceptions;

namespace PodkarpackiLekarz.Application.Exceptions.Users
{
    public class InvalidRefreshTokenException : AuthorizationException
    {
        public InvalidRefreshTokenException()
            : base("Invalid refresh token")
        {
        }
    }
}
