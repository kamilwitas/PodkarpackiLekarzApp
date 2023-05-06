using PodkarpackiLekarz.Shared.Exceptions;

namespace PodkarpackiLekarz.Core.Users.Exceptions;

public class IncorrectPasswordException : BadRequestException
{
    public IncorrectPasswordException() : base("Incorrect password")
    {
    }
}