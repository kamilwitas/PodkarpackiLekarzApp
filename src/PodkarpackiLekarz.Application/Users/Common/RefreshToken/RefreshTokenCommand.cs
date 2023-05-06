using MediatR;
using PodkarpackiLekarz.Application.Auth;

namespace PodkarpackiLekarz.Application.Users.Common.RefreshToken;
public class RefreshTokenCommand : IRequest<AuthDto>
{
    public string ExpiredAccessToken { get; private set; }
    public string RefreshToken { get; private set; }

    public RefreshTokenCommand(
        string expiredAccessToken,
        string refreshToken)
    {
        ExpiredAccessToken = expiredAccessToken;
        RefreshToken = refreshToken;
    }
}
