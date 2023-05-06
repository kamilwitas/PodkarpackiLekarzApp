using PodkarpackiLekarz.Shared.Exceptions;

namespace PodkarpackiLekarz.Application.Exceptions.Users
{
    public class InvalidCredentialsException : BadRequestException
    {
        public InvalidCredentialsException()
            : base("Invalid credentials")
        {
        }
    }
}
