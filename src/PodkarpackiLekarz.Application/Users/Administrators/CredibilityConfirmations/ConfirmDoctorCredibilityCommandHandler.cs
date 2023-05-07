using MediatR;
using PodkarpackiLekarz.Application.Exceptions.Users;
using PodkarpackiLekarz.Core.Users.Doctors;

namespace PodkarpackiLekarz.Application.Users.Administrators.CredibilityConfirmations;
public class ConfirmDoctorCredibilityCommandHandler : IRequestHandler<ConfirmDoctorCredibilityCommand, bool>
{
    private readonly IDoctorsRepository _doctorsRepository;

    public ConfirmDoctorCredibilityCommandHandler(IDoctorsRepository doctorsRepository)
    {
        _doctorsRepository = doctorsRepository;
    }

    public async Task<bool> Handle(ConfirmDoctorCredibilityCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorsRepository.GetAsync(request.DoctorId);

        if (doctor is null)
            throw new DoctorDoesNotExistException(request.DoctorId);

        if (doctor.CredibilityConfirmed)
            return true;

        doctor.ConfirmDoctorCredibility();

        _doctorsRepository.Update(doctor);
        await _doctorsRepository.SaveAsync();

        return doctor.CredibilityConfirmed;
    }
}
