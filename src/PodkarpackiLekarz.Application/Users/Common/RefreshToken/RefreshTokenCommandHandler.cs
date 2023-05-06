using MediatR;
using PodkarpackiLekarz.Application.Auth;

namespace PodkarpackiLekarz.Application.Users.Common.RefreshToken;
public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthDto>
{
    private readonly IAuthorizationService _authorizationService;

    public RefreshTokenCommandHandler(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    public async Task<AuthDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var authDto = await _authorizationService.RefreshTokenAsync(
                request.ExpiredAccessToken,
                request.RefreshToken);

        return authDto;
    }
}
