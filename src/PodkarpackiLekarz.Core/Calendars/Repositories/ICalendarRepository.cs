namespace PodkarpackiLekarz.Core.Calendars.Repositories
{
    public interface ICalendarRepository
    {
        Task<Day> GetDayAsync(DateOnly dateOnly);
        Task<Day> GetDayAsync(Guid slotId);
        Task AddDayAsync(Day day);
        Task UpdateDayAsync(Day day);
        Task SaveAsync();
    }
}
