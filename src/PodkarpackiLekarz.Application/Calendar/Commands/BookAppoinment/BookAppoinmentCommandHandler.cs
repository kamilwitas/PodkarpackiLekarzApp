using MediatR;
using PodkarpackiLekarz.Application.Auth;
using PodkarpackiLekarz.Application.Calendar.Exceptions;
using PodkarpackiLekarz.Core.Calendars.Repositories;

namespace PodkarpackiLekarz.Application.Calendar.Commands.BookAppoinment
{
    public class BookAppoinmentCommandHandler : IRequestHandler<BookAppoinmentCommand>
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly IApplicationPrincipalService _applicationPrincipalService;

        public BookAppoinmentCommandHandler(
            ICalendarRepository calendarRepository,
            IApplicationPrincipalService applicationPrincipalService)
        {
            _calendarRepository = calendarRepository;
            _applicationPrincipalService = applicationPrincipalService;
        }

        public async Task Handle(BookAppoinmentCommand request, CancellationToken cancellationToken)
        {
            var day = await _calendarRepository.GetDayAsync(request.SlotId);

            if (day is null)
                throw new SlotNotFoundException(request.SlotId);

            var patientId = _applicationPrincipalService.GetUserId();

            day.BookAppoinment(request.SlotId, patientId, DateTime.Now);

            await _calendarRepository.UpdateDayAsync(day);
            await _calendarRepository.SaveAsync();
        }
    }
}
