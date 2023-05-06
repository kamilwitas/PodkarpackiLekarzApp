using PodkarpackiLekarz.Shared.Exceptions;

namespace PodkarpackiLekarz.Application.Exceptions.Users;
public class CredibilityIsNotConfirmedException : ForbiddenException
{
    public CredibilityIsNotConfirmedException() : base("Credibility is not confirmed")
    {
    }
}
