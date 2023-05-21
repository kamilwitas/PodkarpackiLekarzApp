using MediatR;

namespace PodkarpackiLekarz.Application.Users.Doctors.CredibilityConfirmations;

public class RejectDoctorCredibilityCommand : IRequest
{
    public Guid DoctorId { get; private set; }

    public RejectDoctorCredibilityCommand(Guid doctorId)
    {
        DoctorId = doctorId;
    }
}