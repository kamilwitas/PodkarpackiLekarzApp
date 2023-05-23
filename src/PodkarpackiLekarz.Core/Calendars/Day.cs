using PodkarpackiLekarz.Core.Calendars.Exceptions;

namespace PodkarpackiLekarz.Core.Calendars;

public class Day
{
    public Guid Id { get; private set; }
    public DateOnly Date { get; private set; }
    public Guid DoctorId { get; private set; }
    public List<Slot> Slots { get; private set; }

    public Day()
    { }

    private Day(
        Guid id,
        DateOnly date,
        Guid doctorId,
        List<Slot> slots = null)
    {
        Id = id;
        Date = date;
        DoctorId = doctorId;
        Slots = slots ?? new List<Slot>();
    }

    public void AddSlots(IEnumerable<Slot> slots)
    {
        foreach (var newSlot in slots)
        {
            CheckIfSlotCanBeAdded(newSlot);
            
            Slots.Add(newSlot);
        }
    }

    private void CheckIfSlotCanBeAdded(Slot slot)
    {
        Func<Slot, bool> condition = new(x => ((x.Timeframe.StartTime > slot.Timeframe.StartTime &&
                                                        x.Timeframe.EndTime > slot.Timeframe.StartTime &&
                                                        x.Timeframe.StartTime >= slot.Timeframe.EndTime &&
                                                        x.Timeframe.EndTime > slot.Timeframe.EndTime) ||
                                                    (x.Timeframe.StartTime < slot.Timeframe.StartTime &&
                                                     x.Timeframe.EndTime <= slot.Timeframe.StartTime &&
                                                     x.Timeframe.StartTime < slot.Timeframe.EndTime &&
                                                     x.Timeframe.EndTime < slot.Timeframe.EndTime)));

        if (Slots.Any(condition))
            throw new TimeframeUnavailableException(slot.Timeframe);
    }
}