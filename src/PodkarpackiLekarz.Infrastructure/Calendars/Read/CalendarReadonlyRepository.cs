using Dapper;
using PodkarpackiLekarz.Application.Calendar.Dtos.Public;
using PodkarpackiLekarz.Application.Calendar.Read;
using PodkarpackiLekarz.Shared.Persistence;

namespace PodkarpackiLekarz.Infrastructure.Calendars.Read
{
    public class CalendarReadonlyRepository
        : ICalendarReadonlyRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public CalendarReadonlyRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<CalendarPublicDto?> GetDoctorPublicCalendarAsync(Guid doctorId, DateTime? from, DateTime? to)
        {
            var fromDateTime = from;
            var toDateTime = to;

            if (from is null)
                fromDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            if (to is null)
                toDateTime = new DateTime(DateTime.Now.AddDays(7).Year, DateTime.Now.AddDays(7).Month, DateTime.Now.AddDays(7).Day, 23, 59, 59);

            var sql = $@"select 
                            doctors.Id {nameof(PublicCalendarDbEntry.DoctorId)},
                            iu.FirstName {nameof(PublicCalendarDbEntry.DoctorFirstName)},
                            iu.LastName {nameof(PublicCalendarDbEntry.DoctorLastName)},
                            days.Id {nameof(PublicCalendarDbEntry.DayId)},
                            days.Date {nameof(PublicCalendarDbEntry.Date)},
                            slots.Id {nameof(PublicCalendarDbEntry.SlotId)},
                            slots.Timeframe_StartTime {nameof(PublicCalendarDbEntry.StartTime)},
                            slots.Timeframe_EndTime {nameof(PublicCalendarDbEntry.EndTime)},
                            IsAvailable = 
                                case
                                    when slots.Id is null then null
                                    when exists (select * from {AppSchema.Value}.Appoinments ap where ap.SlotId = slots.Id and ap.Status in (0,1,3)) then 0
                                    else 1
                                end
                            from
                            {AppSchema.Value}.Doctors doctors
                            left join {AppSchema.Value}.Days
                                on doctors.Id = doctors.Id
                                and days.Date >= @FromDate
                                and days.Date <= @ToDate
                            left join {AppSchema.Value}.IdentityUsers iu
                                on iu.Id = doctors.Id
                            left join {AppSchema.Value}.Slots slots
                                on slots.DayId = days.id
                                and slots.TimeFrame_StartTime >= @StartTime
                                and slots.TimeFrame_EndTime <= @EndTime
                            where doctors.Id = @DoctorId
                            order by days.Date asc, slots.TimeFrame_StartTime asc";

            var parameters = new
            {
                DoctorId = doctorId,
                FromDate = fromDateTime!.Value.Date,
                ToDate = toDateTime!.Value.Date,
                StartTime = TimeOnly.FromDateTime(fromDateTime.Value).ToString(),
                EndTime = TimeOnly.FromDateTime(toDateTime.Value).ToString()
            };

            var connection = _connectionFactory.GetOpenConnection();
            var results = await connection.QueryAsync<PublicCalendarDbEntry>(sql, parameters);

            return CreateCalendarPublicDto(results.ToList());
        }

        private CalendarPublicDto? CreateCalendarPublicDto(List<PublicCalendarDbEntry> dbEntries)
        {
            if (dbEntries is null || !dbEntries.Any())
                return null;

            var filteredDbEntries = new List<PublicCalendarDbEntry>();

            dbEntries.ForEach(x =>
            {
                if (x.DayId is not null && x.SlotId is not null)
                    filteredDbEntries.Add(x);
            });

            if (!filteredDbEntries.Any())
                return null;

            var resultsByDayId = filteredDbEntries.ToDictionary(x => x.DayId);
            var resultsBySlotId = filteredDbEntries.ToDictionary(x => x.SlotId);

            var dayGroups = filteredDbEntries.GroupBy(x => x.DayId).ToList();
            var days = new List<DayPublicDto>();
            foreach (var dayGroup in dayGroups)
            {
                var slotIds = dayGroup.Select(x => x.SlotId).ToList();

                var slots = new List<SlotPublicDto>();

                slotIds.ForEach(x =>
                {
                    var entry = resultsBySlotId[x];
                    var slot = new SlotPublicDto(x!.Value, TimeOnly.FromTimeSpan(entry.StartTime!.Value), TimeOnly.FromTimeSpan(entry.EndTime!.Value), entry.IsAvailable!.Value);
                    slots.Add(slot);
                });

                var dayEntry = resultsByDayId[dayGroup.Key];
                days.Add(new DayPublicDto(dayEntry.DayId!.Value, DateOnly.FromDateTime(dayEntry.Date!.Value), slots));
            }

            var doctor = filteredDbEntries.Select(x => new { Id = x.DoctorId, FirstName = x.DoctorFirstName, LastName = x.DoctorLastName }).FirstOrDefault();
            var calendarPublicDto = new CalendarPublicDto(
                doctor!.Id.Value, doctor.FirstName, doctor.LastName, days);

            return calendarPublicDto;
        }
    }
}
