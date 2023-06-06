using Microsoft.EntityFrameworkCore;
using PodkarpackiLekarz.Core.Calendars;

namespace PodkarpackiLekarz.Infrastructure.Persistence.Repositories.Calendars
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly AppDbContext _appDbContext;

        public CalendarRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddDayAsync(Day day)
            => await _appDbContext.Days.AddAsync(day);

        public async Task<Day?> GetDayAsync(DateOnly dateOnly)
            => await _appDbContext.Days.FirstOrDefaultAsync(x => x.Date == dateOnly);

        public async Task SaveAsync()
            => await _appDbContext.SaveChangesAsync();

        public void UpdateDayAsync(Day day)
            => _appDbContext.Days.Update(day);
    }
}
