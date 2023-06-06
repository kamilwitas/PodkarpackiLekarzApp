namespace PodkarpackiLekarz.Application.Calendar.Services
{
    public interface IScheduleCreator
    {
        Task<Guid> CreateSlotAsync(DateTime startDateTime, DateTime endDateTime, Guid doctorId);
    }        
}
