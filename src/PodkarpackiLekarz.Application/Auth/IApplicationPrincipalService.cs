using PodkarpackiLekarz.Shared.Identity;

namespace PodkarpackiLekarz.Application.Auth
{
    public interface IApplicationPrincipalService
    {
        Guid GetUserId();
        Role GetUserRole();
    }
}
