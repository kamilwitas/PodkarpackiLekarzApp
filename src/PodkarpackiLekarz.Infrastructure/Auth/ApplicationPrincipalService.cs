using Microsoft.AspNetCore.Http;
using PodkarpackiLekarz.Application.Auth;
using System.Security.Claims;

namespace PodkarpackiLekarz.Infrastructure.Auth
{
    public class ApplicationPrincipalService : IApplicationPrincipalService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationPrincipalService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext?
                .User?
                .Claims?
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            return Guid.Parse(userId!);
        }
    }
}
