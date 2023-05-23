using PodkarpackiLekarz.Core.Calendars.Exceptions;

namespace PodkarpackiLekarz.Core.Calendars;

public class Timeframe
{
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }

    public Timeframe()
    {
        
    }
    
    private Timeframe(
        TimeOnly startTime,
        TimeOnly endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
    }

    public static Timeframe Create(
        TimeOnly startTime,
        TimeOnly endTime)
    {
        if (startTime >= endTime)
            throw new InvalidTimeframeException("Start time cannot be newer than end time");

        if (startTime.Minute % 15 != 0 || endTime.Minute % 15 != 0)
            throw new InvalidTimeframeException("Invalid timeframe format");

        var startTimeNew = new TimeOnly(startTime.Hour, startTime.Minute);
        var endTimeNew = new TimeOnly(endTime.Hour, endTime.Minute);

        return new Timeframe(startTimeNew, endTimeNew);
    }
}