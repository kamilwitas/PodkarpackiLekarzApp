using MediatR;
using Microsoft.AspNetCore.Identity;
using PodkarpackiLekarz.Application.Exceptions.Users;
using PodkarpackiLekarz.Core.Users;
using PodkarpackiLekarz.Core.Users.Patients;

namespace PodkarpackiLekarz.Application.Users.Patients.Register;
public class RegisterPatientCommandHandler : IRequestHandler<RegisterPatientCommand, Guid>
{
    private readonly IPatientsRepository _patientsRepository;
    private readonly IIdentityUsersRepository _identityUsersRepository;
    private readonly IPasswordHasher<Patient> _passwordHasher;

    public RegisterPatientCommandHandler(
        IPatientsRepository patientsRepository,
        IIdentityUsersRepository identityUsersRepository,
        IPasswordHasher<Patient> passwordHasher)
    {
        _patientsRepository = patientsRepository;
        _identityUsersRepository = identityUsersRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(RegisterPatientCommand request, CancellationToken cancellationToken)
    {
        if (await _identityUsersRepository.IsExist(request.Email))
            throw new EmailIsInUseException(request.Email);                    

        var patient = UsersFactory.CreatePatient(
            request.FirstName,
            request.LastName,
            request.Email,
            request.DateOfBirth,
            request.Pesel);

        var hashedPassword = _passwordHasher.HashPassword(patient, request.Password);

        patient.SetPassword(hashedPassword);

        await _patientsRepository.AddAsync(patient);
        await _patientsRepository.SaveAsync();

        return patient.Id;
    }
}
