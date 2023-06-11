namespace PodkarpackiLekarz.Api.Requests.Calendars
{
    public class GetPublicDoctorCalendarRequest
    {
        public Guid DoctorId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
