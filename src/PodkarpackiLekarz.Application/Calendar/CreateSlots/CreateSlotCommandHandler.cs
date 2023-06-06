using MediatR;
using PodkarpackiLekarz.Application.Calendar.Services;
using PodkarpackiLekarz.Application.Exceptions.Users;
using PodkarpackiLekarz.Core.Users.Doctors;

namespace PodkarpackiLekarz.Application.Calendar.CreateSlots
{
    public class CreateSlotCommandHandler : IRequestHandler<CreateSlotCommand, Guid>
    {
        private readonly IScheduleCreator _scheduleCreator;
        private readonly IDoctorsRepository _doctorsRepository;

        public CreateSlotCommandHandler(
            IScheduleCreator scheduleCreator,
            IDoctorsRepository doctorsRepository)
        {
            _scheduleCreator = scheduleCreator;
            _doctorsRepository = doctorsRepository;
        }

        async Task<Guid> IRequestHandler<CreateSlotCommand, Guid>.Handle(CreateSlotCommand request, CancellationToken cancellationToken)
        {
            if (!await _doctorsRepository.IsExist(request.DoctorId))
                throw new DoctorDoesNotExistException(request.DoctorId);

            return await _scheduleCreator.CreateSlotAsync(request.StartDateTime, request.EndDateTime, request.DoctorId);
        }
            
    }
}
