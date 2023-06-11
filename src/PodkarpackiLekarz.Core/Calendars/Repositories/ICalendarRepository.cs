namespace PodkarpackiLekarz.Core.Calendars.Repositories
{
    public interface ICalendarRepository
    {
        Task<Day> GetDayAsync(DateOnly dateOnly);
        Task AddDayAsync(Day day);
        void UpdateDayAsync(Day day);
        Task SaveAsync();
    }
}
