using MediatR;
using Microsoft.AspNetCore.Identity;
using PodkarpackiLekarz.Application.Exceptions.Users;
using PodkarpackiLekarz.Core.Users;
using PodkarpackiLekarz.Core.Users.Doctors;

namespace PodkarpackiLekarz.Application.Users.Doctors.Register;
public class RegisterDoctorCommandHandler : IRequestHandler<RegisterDoctorCommand, Guid>
{
    private readonly IDoctorsRepository _doctorsRepository;
    private readonly IIdentityUsersRepository _identityUsersRepository;
    private readonly IDoctorTypesRepository _doctorTypeRepository;
    private readonly IPasswordHasher<Doctor> _passwordHasher;

    public RegisterDoctorCommandHandler(
        IDoctorsRepository doctorsRepository,
        IIdentityUsersRepository identityUsersRepository,
        IDoctorTypesRepository doctorTypeRepository,
        IPasswordHasher<Doctor> passwordHasher)
    {
        _doctorsRepository = doctorsRepository;
        _identityUsersRepository = identityUsersRepository;
        _doctorTypeRepository = doctorTypeRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(RegisterDoctorCommand request, CancellationToken cancellationToken)
    {
        if (await _identityUsersRepository.IsExist(request.Email))
            throw new EmailIsInUseException(request.Email);

        var doctorType = await _doctorTypeRepository.GetAsync(request.DoctorTypeId);

        if (doctorType is null)
            throw new DoctorTypeNotFoundException(request.DoctorTypeId);

        var doctor = UsersFactory.CreateDoctor(
            request.FirstName,
            request.LastName,
            request.Email,
            doctorType,
            request.Description,
            request.MedicalLicenseNumber);

        var hashedPassword = _passwordHasher.HashPassword(doctor, request.Password);

        doctor.SetPassword(hashedPassword);

        await _doctorsRepository.AddAsync(doctor);
        await _doctorsRepository.SaveAsync();

        return doctor.Id;
    }
}
