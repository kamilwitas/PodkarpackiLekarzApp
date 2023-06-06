namespace PodkarpackiLekarz.Api.Requests.Calendars
{
    public record CreateSlotRequest(Guid DoctorId, DateTime startDateTime, DateTime endDateTime);        
}
