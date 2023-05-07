using PodkarpackiLekarz.Shared.Exceptions;

namespace PodkarpackiLekarz.Application.Exceptions.Users;

public class SpecialityAlreadyExistsException : BadRequestException
{
    public SpecialityAlreadyExistsException(string speciality) 
        : base($"Speciality: {speciality} already exists")
    {
    }
}