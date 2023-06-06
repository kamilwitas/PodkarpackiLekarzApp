namespace PodkarpackiLekarz.Core.Calendars.Exceptions
{
    public class SlotUnavailableException : Exception
    {
        public SlotUnavailableException(Guid slotId)
            : base($"Slot: {slotId.ToString()} is currently unavailable")
        {
            
        }
    }
}
