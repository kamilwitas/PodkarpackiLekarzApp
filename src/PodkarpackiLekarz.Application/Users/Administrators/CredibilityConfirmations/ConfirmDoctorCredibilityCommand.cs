using MediatR;

namespace PodkarpackiLekarz.Application.Users.Administrators.CredibilityConfirmations;
public class ConfirmDoctorCredibilityCommand : IRequest<bool>
{
    public Guid DoctorId { get; private set; }

    public ConfirmDoctorCredibilityCommand(Guid doctorId)
    {
        DoctorId = doctorId;
    }
}
