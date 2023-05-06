using Microsoft.AspNetCore.Mvc;

namespace PodkarpackiLekarz.Application.Attributes;
public class CredibilityConfirmationRequirementAttribute : TypeFilterAttribute
{
    public CredibilityConfirmationRequirementAttribute() 
        : base(typeof(CredibilityConfirmationRequirementAuthorizationFilter))
    {
    }
}
