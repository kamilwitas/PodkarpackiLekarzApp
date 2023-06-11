using PodkarpackiLekarz.Application.Calendar.Dtos.Public;

namespace PodkarpackiLekarz.Application.Calendar.Queries.GetDoctorsPublicCalendar
{
    public class GetDoctorPublicCalendarResult
    {
        public CalendarPublicDto Calendar { get; private set; }

        public GetDoctorPublicCalendarResult(CalendarPublicDto calendar)
        {
            Calendar = calendar;
        }
    }
}
