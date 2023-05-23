namespace PodkarpackiLekarz.Core.Calendars;

public class Slot
{
    public Guid Id { get; private set; }
    public Timeframe Timeframe { get; private set; }
    public bool IsAvailable { get; private set; }

    private int[] GetSlotSize()
    {
        
    }
}