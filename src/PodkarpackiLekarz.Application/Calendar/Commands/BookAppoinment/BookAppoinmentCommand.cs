using MediatR;

namespace PodkarpackiLekarz.Application.Calendar.Commands.BookAppoinment
{
    public class BookAppoinmentCommand : IRequest
    {
        public Guid SlotId { get; private set; }

        public BookAppoinmentCommand(Guid slotId)
        {
            SlotId = slotId;
        }
    }
}
