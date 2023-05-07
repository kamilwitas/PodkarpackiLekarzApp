using PodkarpackiLekarz.Shared.Exceptions;

namespace PodkarpackiLekarz.Application.Exceptions.Users;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(Guid userId) 
        : base($"User not found")
    {
    }
}