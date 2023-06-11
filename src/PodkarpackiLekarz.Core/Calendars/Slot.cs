using PodkarpackiLekarz.Core.Calendars.Exceptions;

namespace PodkarpackiLekarz.Core.Calendars;

public class Slot
{
    public Guid Id { get; private set; }
    public Timeframe Timeframe { get; private set; }
    public List<Appoinment> Appoinments { get; private set; }
    public bool IsAvailable => CheckIsAvailable();

    public Slot()
    {
        
    }

    private Slot(
        Guid id,
        Timeframe timeframe)
    {
        Id = id;
        Timeframe = timeframe;
        Appoinments = new List<Appoinment>();
    }

    public static Slot Create(
        Timeframe timeFrame)
        => new Slot(
            Guid.NewGuid(),
            timeFrame);

    internal void BookAppoinment(
        Guid patientId,
        DateTime currentDateTime)
    {
        if (!CheckIsAvailable())
            throw new SlotUnavailableException(Id);

        Appoinments.Add(Appoinment.Create(patientId, currentDateTime));        
    }

    private bool CheckIsAvailable()
        => !Appoinments.Any(x => x.IsAccepted || x.IsWaiting || x.IsClosed); 
}