using Microsoft.AspNetCore.Mvc.Filters;
using PodkarpackiLekarz.Application.Auth;
using PodkarpackiLekarz.Application.Exceptions.Users;
using PodkarpackiLekarz.Core.Users.Doctors;
using PodkarpackiLekarz.Shared.Identity;

namespace PodkarpackiLekarz.Application.Attributes;
public class CredibilityConfirmationRequirementAuthorizationFilter : IAsyncAuthorizationFilter
{
	private readonly IApplicationPrincipalService _applicationPrincipalService;
	private readonly IDoctorsRepository _doctorsRepository;    

    public CredibilityConfirmationRequirementAuthorizationFilter(
        IApplicationPrincipalService applicationPrincipalService,
        IDoctorsRepository doctorsRepository)
    {
        _applicationPrincipalService = applicationPrincipalService;
        _doctorsRepository = doctorsRepository;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var loggedUserRole = _applicationPrincipalService.GetUserRole();
        var loggedUserId = _applicationPrincipalService.GetUserId();

        if (loggedUserRole == Role.Doctor)
        {
            var doctorCredibilityConfirmationStatus = await _doctorsRepository.IsCredibilityConfirmed(loggedUserId);

            if (doctorCredibilityConfirmationStatus is null ||
                !doctorCredibilityConfirmationStatus.Value)
            {
                throw new CredibilityIsNotConfirmedException();
            }
        }
    }
}
