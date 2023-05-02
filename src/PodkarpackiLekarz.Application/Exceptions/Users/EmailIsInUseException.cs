using PodkarpackiLekarz.Shared.Exceptions;

namespace PodkarpackiLekarz.Application.Exceptions.Users;
public class EmailIsInUseException : BadRequestException
{
    public EmailIsInUseException(string email) 
        : base($"Email: {email} is in use")
    {
    }
}
