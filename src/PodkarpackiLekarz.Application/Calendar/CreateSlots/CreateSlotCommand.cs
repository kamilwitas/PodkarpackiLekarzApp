using MediatR;

namespace PodkarpackiLekarz.Application.Calendar.CreateSlots
{
    public class CreateSlotCommand : IRequest<Guid>
    {
        public Guid DoctorId { get; private set; }
        public DateTime StartDateTime { get; private set; }
        public DateTime EndDateTime { get; private set; }

        public CreateSlotCommand(
            Guid doctorId,
            DateTime startDateTime,
            DateTime endDateTime)
        {
            DoctorId = doctorId;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
        }
    }
}
