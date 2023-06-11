using MediatR;
using PodkarpackiLekarz.Application.Calendar.Read;

namespace PodkarpackiLekarz.Application.Calendar.Queries.GetDoctorsPublicCalendar
{
    public class GetDoctorPublicCalendarQueryHandler
        : IRequestHandler<GetDoctorPublicCalendarQuery, GetDoctorPublicCalendarResult?>
    {
        private readonly ICalendarReadonlyRepository _calendarReadonlyRepository;

        public GetDoctorPublicCalendarQueryHandler(ICalendarReadonlyRepository calendarReadonlyRepository)
        {
            _calendarReadonlyRepository = calendarReadonlyRepository;
        }

        public async Task<GetDoctorPublicCalendarResult> Handle(GetDoctorPublicCalendarQuery request, CancellationToken cancellationToken)
        {
            var calendarPublicDto = await _calendarReadonlyRepository.GetDoctorPublicCalendarAsync(
                request.DoctorId, request.From, request.To);

            if (calendarPublicDto == null)
                return null;

            var doctorPublicCalendarResult = new GetDoctorPublicCalendarResult(calendarPublicDto);

            return doctorPublicCalendarResult;                       
        }
    }
}
