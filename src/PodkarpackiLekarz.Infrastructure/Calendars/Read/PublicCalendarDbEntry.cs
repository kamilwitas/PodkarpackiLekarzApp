namespace PodkarpackiLekarz.Infrastructure.Calendars.Read
{
    public class PublicCalendarDbEntry
    {
        public Guid? DoctorId { get; set; }
        public string? DoctorFirstName { get; set; }
        public string? DoctorLastName { get; set; }
        public Guid? DayId { get; set; }
        public DateTime? Date { get; set; }
        public Guid? SlotId { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public bool? IsAvailable { get; set; }

        public PublicCalendarDbEntry(
            Guid? doctorId,
            string? doctorFirstName,
            string? doctorLastName,
            Guid? dayId,
            DateTime? date,
            Guid? slotId,
            TimeSpan? startTime,
            TimeSpan? endTime,
            bool? isAvailable)
        {
            DoctorId = doctorId;
            DoctorFirstName = doctorFirstName;
            DoctorLastName = doctorLastName;
            DayId = dayId;
            Date = date;
            SlotId = slotId;
            StartTime = startTime;
            EndTime = endTime;
            IsAvailable = isAvailable;
        }

        public PublicCalendarDbEntry()
        {
            
        }
    }
}
