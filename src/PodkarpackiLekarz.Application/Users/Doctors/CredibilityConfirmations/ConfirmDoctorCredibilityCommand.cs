using MediatR;

namespace PodkarpackiLekarz.Application.Users.Doctors.CredibilityConfirmations;
public class ConfirmDoctorCredibilityCommand : IRequest<bool>
{
    public Guid DoctorId { get; private set; }

    public ConfirmDoctorCredibilityCommand(Guid doctorId)
    {
        DoctorId = doctorId;
    }
}
