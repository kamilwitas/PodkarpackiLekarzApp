using MediatR;

namespace PodkarpackiLekarz.Application.Users.Doctors.AddDoctorType;

public class AddDoctorTypeCommand : IRequest<Guid>
{
    public string Speciality { get; private set; }

    public AddDoctorTypeCommand(string speciality)
    {
        Speciality = speciality;
    }
}