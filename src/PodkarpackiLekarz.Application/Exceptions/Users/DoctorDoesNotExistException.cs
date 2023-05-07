using PodkarpackiLekarz.Shared.Exceptions;

namespace PodkarpackiLekarz.Application.Exceptions.Users;
public class DoctorDoesNotExistException : BadRequestException
{
    public DoctorDoesNotExistException(Guid doctorId)
        : base($"Doctor with id: {doctorId.ToString()} does not exist")
    {
    }
}
