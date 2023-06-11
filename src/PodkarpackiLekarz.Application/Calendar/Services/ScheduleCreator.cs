using PodkarpackiLekarz.Application.Calendar.Exceptions;
using PodkarpackiLekarz.Core.Calendars;
using PodkarpackiLekarz.Core.Calendars.Repositories;

namespace PodkarpackiLekarz.Application.Calendar.Services
{
    public class ScheduleCreator : IScheduleCreator
    {
        private readonly ICalendarRepository _calendarRepository;

        public ScheduleCreator(ICalendarRepository calendarRepository)
        {
            _calendarRepository = calendarRepository;
        }

        public async Task<Guid> CreateSlotAsync(DateTime startDateTime, DateTime endDateTime, Guid doctorId)
        {
            if ((startDateTime.Day != endDateTime.Day)
                || (startDateTime.Month != endDateTime.Month)
                || (startDateTime.Year != endDateTime.Year))
                throw new InvalidTimeframeSizeException();

            var timeFrame = Timeframe.Create(TimeOnly.FromDateTime(startDateTime), TimeOnly.FromDateTime(endDateTime));
            var slot = Slot.Create(timeFrame);

            var day = await _calendarRepository.GetDayAsync(DateOnly.FromDateTime(startDateTime));

            if (day is null)
            {
                day = Day.Create(DateOnly.FromDateTime(startDateTime), doctorId);                
                day.AddSingleSlot(slot);                
                await _calendarRepository.AddDayAsync(day);
                await _calendarRepository.SaveAsync();
                return slot.Id;
            }

            day.AddSingleSlot(slot);
            _calendarRepository.UpdateDayAsync(day);
            await _calendarRepository.SaveAsync();
            return slot.Id;
        }
    }
}
