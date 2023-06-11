namespace PodkarpackiLekarz.Application.Calendar.Dtos.Public
{
    public class CalendarPublicDto
    {
        public Guid DoctorId { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public List<DayPublicDto> Days { get; set; } = new();

        public CalendarPublicDto(Guid doctorId, string doctorFirstName, string doctorLastName, List<DayPublicDto> days)
        {
            DoctorId = doctorId;
            DoctorFirstName = doctorFirstName;
            DoctorLastName = doctorLastName;
            Days = days;
        }
    }
}
