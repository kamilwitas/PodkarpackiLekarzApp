using PodkarpackiLekarz.Shared.Exceptions;

namespace PodkarpackiLekarz.Core.Calendars.Exceptions;

public class TimeframeUnavailableException : BadRequestException
{
    public TimeframeUnavailableException(Timeframe timeFrame) 
        : base($"Timeframe with start time: {timeFrame.StartTime} and {timeFrame.EndTime} cannot be added")
    {
    }
}