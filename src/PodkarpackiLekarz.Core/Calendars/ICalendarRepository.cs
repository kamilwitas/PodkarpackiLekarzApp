namespace PodkarpackiLekarz.Core.Calendars
{
    public interface ICalendarRepository
    {
        Task<Day?> GetDayAsync(DateOnly dateOnly);
        Task AddDayAsync(Day day);
        void UpdateDayAsync(Day day);
        Task SaveAsync();        
    }
}
