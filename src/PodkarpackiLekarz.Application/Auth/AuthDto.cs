namespace PodkarpackiLekarz.Application.Auth;

public class AuthDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpiresAt { get; set; }
}
