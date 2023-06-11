using PodkarpackiLekarz.Shared.Exceptions;

namespace PodkarpackiLekarz.Application.Calendar.Exceptions
{
    public class SlotNotFoundException : NotFoundException
    {
        public SlotNotFoundException(Guid slotId) 
            : base($"Slot: {slotId.ToString()} not found")
        {
        }
    }
}
