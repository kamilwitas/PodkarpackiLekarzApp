using MediatR;
using PodkarpackiLekarz.Application.Exceptions.Users;
using PodkarpackiLekarz.Core.Users.Doctors;

namespace PodkarpackiLekarz.Application.Users.Doctors.CredibilityConfirmations;

public class RejectDoctorCredibilityCommandHandler : IRequestHandler<RejectDoctorCredibilityCommand>
{
    private readonly IDoctorsRepository _doctorsRepository;

    public RejectDoctorCredibilityCommandHandler(IDoctorsRepository doctorsRepository)
    {
        _doctorsRepository = doctorsRepository;
    }

    public async Task Handle(RejectDoctorCredibilityCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorsRepository.GetAsync(request.DoctorId);

        if (doctor is null)
            throw new DoctorDoesNotExistException(request.DoctorId);

        doctor.RejectDoctorCredibility();

        _doctorsRepository.Update(doctor);
        await _doctorsRepository.SaveAsync();
    }
}