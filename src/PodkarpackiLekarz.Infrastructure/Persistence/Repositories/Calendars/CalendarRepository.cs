using Microsoft.EntityFrameworkCore;
using PodkarpackiLekarz.Core.Calendars;
using PodkarpackiLekarz.Core.Calendars.Repositories;

namespace PodkarpackiLekarz.Infrastructure.Persistence.Repositories.Calendars
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly AppDbContext _appDbContext;
        private Guid? _transactionId;

        public CalendarRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddDayAsync(Day day) 
        {
            await BeginTransaction();
            await _appDbContext.Days.AddAsync(day);
        }

        public async Task<Day?> GetDayAsync(DateOnly dateOnly)
            => await _appDbContext.Days.FirstOrDefaultAsync(x => x.Date == dateOnly);

        public async Task<Day?> GetDayAsync(Guid slotId)
        {
            var day = await _appDbContext.Days.FirstOrDefaultAsync(x => x.Slots.Any(x => x.Id == slotId));
            return day;
        }

        public async Task SaveAsync() 
        {
            await _appDbContext.SaveChangesAsync();
            await CommitTransaction();
        }

        public async Task UpdateDayAsync(Day day) 
        {
            await BeginTransaction();
            _appDbContext.Days.Update(day);
        } 

        private async Task BeginTransaction()
        {
            if (_transactionId is not null)
                return;

            var transaction = await _appDbContext.Database.BeginTransactionAsync();
            
            _transactionId = transaction.TransactionId;
        }

        private async Task CommitTransaction()
        {
            if (_transactionId is null)
                return;

            var transaction = _appDbContext.Database.CurrentTransaction;

            await transaction!.CommitAsync();

            await transaction.DisposeAsync();
        }
    }
}
