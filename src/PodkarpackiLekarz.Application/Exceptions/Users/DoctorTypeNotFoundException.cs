using PodkarpackiLekarz.Shared.Exceptions;

namespace PodkarpackiLekarz.Application.Exceptions.Users;
public class DoctorTypeNotFoundException : BadRequestException
{
    public DoctorTypeNotFoundException(string doctorTypeId) : base(doctorTypeId)
    {
    }
}
