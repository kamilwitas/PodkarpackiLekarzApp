namespace PodkarpackiLekarz.Application.Auth;

public interface IAuthorizationService
{
    Task<AuthDto> SignInAsync(string email, string password);
    Task<AuthDto> RefreshTokenAsync(string expiredAccessToken, string refreshToken);
}
