using MediatR;
using PodkarpackiLekarz.Application.Exceptions.Users;
using PodkarpackiLekarz.Core.Users.Doctors;

namespace PodkarpackiLekarz.Application.Users.Doctors.AddDoctorType;

public class AddDoctorTypeCommandHandler : IRequestHandler<AddDoctorTypeCommand, Guid>
{
    private readonly IDoctorTypesRepository _doctorTypesRepository;

    public AddDoctorTypeCommandHandler(IDoctorTypesRepository doctorTypesRepository)
    {
        _doctorTypesRepository = doctorTypesRepository;
    }

    public async Task<Guid> Handle(AddDoctorTypeCommand request, CancellationToken cancellationToken)
    {
        var doctorType = await _doctorTypesRepository.GetAsync(request.Speciality);

        if (doctorType is not null)
            throw new SpecialityAlreadyExistsException(request.Speciality);

        doctorType = DoctorType.Create(request.Speciality);

        await _doctorTypesRepository.AddAsync(doctorType);
        await _doctorTypesRepository.SaveAsync();

        return doctorType.Id;
    }
}