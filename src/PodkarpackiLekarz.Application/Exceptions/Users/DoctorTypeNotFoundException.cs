using PodkarpackiLekarz.Shared.Exceptions;

namespace PodkarpackiLekarz.Application.Exceptions.Users;
public class DoctorTypeNotFoundException : BadRequestException
{
    public DoctorTypeNotFoundException(Guid doctorTypeId) 
        : base($"Doctor type with id: {doctorTypeId} not found")
    {
    }
}
