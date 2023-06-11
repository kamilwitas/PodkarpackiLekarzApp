using MediatR;

namespace PodkarpackiLekarz.Application.Calendar.Queries.GetDoctorsPublicCalendar
{
    public class GetDoctorPublicCalendarQuery : IRequest<GetDoctorPublicCalendarResult?>
    {
        public Guid DoctorId { get; private set; }
        public DateTime? From { get; private set; }
        public DateTime? To { get; private set; }

        public GetDoctorPublicCalendarQuery(
            Guid doctorId,
            DateTime? from,
            DateTime? to)
        {
            DoctorId = doctorId;
            From = from;
            To = to;
        }
    }
}
