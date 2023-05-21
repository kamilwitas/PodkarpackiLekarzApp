using MediatR;

namespace PodkarpackiLekarz.Application.Users.Doctors.Register;
public class RegisterDoctorCommand : RegisterUserCommandBase, IRequest<Guid>
{
    public Guid DoctorTypeId { get; private set; }
    public string Description { get; private set; }
    public string MedicalLicenseNumber { get; private set; }

    public RegisterDoctorCommand(
        string firstName,
        string lastName,
        string email,
        string password,
        string passwordConfirmation,
        Guid doctorTypeId,
        string description,
        string medicalLicenseNumber)
        : base(firstName, lastName, email, password, passwordConfirmation)
    {
        DoctorTypeId = doctorTypeId;
        Description = description;
        MedicalLicenseNumber = medicalLicenseNumber;
    }
}
