using System.Security.Claims;

namespace PodkarpackiLekarz.Infrastructure.Auth
{
    public interface IJwtProvider
    {
        string CreateAccessToken(IEnumerable<Claim> claims, DateTime currentDateTime, out DateTime expiresAt);
        string CreateRefreshToken(DateTime currentDateTIme, out DateTime validTo);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string expiredAccessToken);
    }
}
