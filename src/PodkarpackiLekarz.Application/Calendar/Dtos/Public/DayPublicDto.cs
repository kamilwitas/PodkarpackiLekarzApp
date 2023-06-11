namespace PodkarpackiLekarz.Application.Calendar.Dtos.Public
{
    public class DayPublicDto
    {
        public Guid DayId { get; set; }
        public DateOnly Date { get; set; }
        public List<SlotPublicDto> Slots { get; set; } = new();

        public DayPublicDto(Guid dayId, DateOnly date, List<SlotPublicDto> slots)
        {
            DayId = dayId;
            Date = date;
            Slots = slots;
        }
    }
}
