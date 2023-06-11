using PodkarpackiLekarz.Application.Calendar.Dtos.Public;

namespace PodkarpackiLekarz.Application.Calendar.Read
{
    public interface ICalendarReadonlyRepository
    {
        Task<CalendarPublicDto> GetDoctorPublicCalendarAsync(
            Guid doctorId,
            DateTime? from,
            DateTime? to);
    }
}
