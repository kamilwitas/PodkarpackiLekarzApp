using PodkarpackiLekarz.Shared.Exceptions;

namespace PodkarpackiLekarz.Core.Calendars.Exceptions;

public class InvalidTimeframeException : BadRequestException
{
    public InvalidTimeframeException(string message) : base(message)
    {
    }
}