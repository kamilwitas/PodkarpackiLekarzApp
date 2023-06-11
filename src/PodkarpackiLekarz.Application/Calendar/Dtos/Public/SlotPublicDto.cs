namespace PodkarpackiLekarz.Application.Calendar.Dtos.Public
{
    public class SlotPublicDto
    {
        public Guid SlotId { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool IsAvailable { get; set; }

        public SlotPublicDto(Guid slotId, TimeOnly startTime, TimeOnly endTime, bool isAvailable)
        {
            SlotId = slotId;
            StartTime = startTime;
            EndTime = endTime;
            IsAvailable = isAvailable;
        }
    }
}
