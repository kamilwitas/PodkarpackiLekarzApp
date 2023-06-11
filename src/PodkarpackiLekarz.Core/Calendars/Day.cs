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

    public static Day Create(
        DateOnly date,
        Guid doctorId)
        => new Day(
            Guid.NewGuid(),
            date,
            doctorId);

    public void AddSlots(IEnumerable<Slot> slots)
    {
        foreach (var newSlot in slots)
        {
            AddSingleSlot(newSlot);
        }
    }

    public void AddSingleSlot(Slot slot)
    {
        if (!CanAddSlot(slot))
            throw new TimeframeUnavailableException(slot.Timeframe);
        Slots.Add(slot);
    }

    public void BookAppoinment(Guid slotId, Guid patientId, DateTime currentDateTime)
    {
        var slot = Slots.FirstOrDefault(s => s.Id == slotId);

        slot.BookAppoinment(patientId, currentDateTime);
    }

    private bool CanAddSlot(Slot slot)
    {
        var existingSlots = Slots
            .Where(x =>
            (slot.Timeframe.StartTime.IsBetween(x.Timeframe.StartTime, x.Timeframe.EndTime)) ||
            (slot.Timeframe.StartTime <= x.Timeframe.StartTime && slot.Timeframe.EndTime > x.Timeframe.StartTime));


        if (existingSlots.Any()) return false;

        return true;
    }

}